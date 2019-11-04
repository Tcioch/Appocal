using Appocal.Models;
using Appocal.ViewModels;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Appocal.Controllers
{
    public class MessageController : Controller
    {
        private ApplicationDbContext _contex;

        public MessageController()
        {
            _contex = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _contex.Dispose();
        }

        public ActionResult MessageBox()
        {
            var userId = HttpContext.User.Identity.GetUserId();
            var messageBox = _contex.Users.Include(u => u.MessageBox.Conversations.Select(c => c.Messages)).Single(u => u.Id == userId).MessageBox;
            var model = new MessageBoxViewModel
            {
                Conversations = new List<ConversationViewModel>()
            };

            messageBox.Conversations = messageBox.Conversations.OrderByDescending(c => c.Messages.Last().MessageDate).ToList();

            foreach(var conv in messageBox.Conversations)
            {
                var convVM = new ConversationViewModel
                {
                    Messages = new List<MessageViewModel>(),
                    SeenBy1 = conv.SeenBy1,
                    SeenBy2 = conv.SeenBy2,
                    User1 = conv.User1,
                    User2 = conv.User2
                };

                foreach(var msg in conv.Messages)
                {
                    var msgVM = new MessageViewModel
                    {
                        Message = msg.Contents,
                        ReceiverName = msg.Receiver,
                        SenderName = msg.Sender
                    };
                    convVM.Messages.Add(msgVM);
                }

                model.Conversations.Add(convVM);
            }

            return View(model);
        }

        [Authorize]
        public ActionResult NewMessageForm(string name)
        {
            if (_contex.Users.Any(u => u.UserName == name))
            {
                var userId = HttpContext.User.Identity.GetUserId();
                var model = new MessageViewModel
                {
                    ReceiverName = name,
                    SenderName = _contex.Users.Single(u => u.Id == userId).UserName
                };
                if (model.ReceiverName == model.SenderName)
                    return RedirectToAction("Index", "Home");
                else
                    return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [Authorize]
        public ActionResult SendMessage(MessageViewModel model)
        {
            var userId = HttpContext.User.Identity.GetUserId();
            var userName = _contex.Users.Single(u => u.Id == userId).UserName;
            Conversation conversation;            if (_contex.Conversations.Any(c => (c.User1 == model.SenderName && c.User2 == model.ReceiverName) || (c.User1 == model.ReceiverName && c.User2 == model.SenderName)))
            {
                conversation = _contex.Conversations.Include(c => c.Messages).First(c => (c.User1 == model.SenderName && c.User2 == model.ReceiverName) || (c.User1 == model.ReceiverName && c.User2 == model.SenderName));
                conversation.SeenBy1 = userName == conversation.User1 ? true : false;
                conversation.SeenBy2 = userName == conversation.User2 ? true : false;
            }
            else
            {
                conversation = new Conversation
                {
                    User1 = model.ReceiverName,
                    User2 = model.SenderName,
                    SeenBy1 = userName == model.ReceiverName ? true : false,
                    SeenBy2 = userName == model.SenderName ? true : false,
                    Messages = new List<Message>()
                };
                _contex.Users.Include(u => u.MessageBox.Conversations).Single(u => u.UserName == model.ReceiverName).MessageBox.Conversations.Add(conversation);
                _contex.Users.Include(u => u.MessageBox.Conversations).Single(u => u.UserName == model.SenderName).MessageBox.Conversations.Add(conversation);
            }

            var message = new Message
            {
                Receiver = model.ReceiverName,
                Sender = model.SenderName,
                MessageDate = DateTime.Now,
                Contents = model.Message
            };

            conversation.Messages.Add(message);

            if (_contex.SaveChanges() > 0)
            {
                return RedirectToAction("MessageBox");
            }
            else
            {
                return RedirectToAction("NewMessageForm", model.ReceiverName);
            }
        }


        public void MessageFromAdmin(string userName, string msg)
        {
            string adminName = "AppoCal";
            Conversation conversation;
            if (_contex.Conversations.Any(c => (c.User1 == adminName && c.User2 == userName) || (c.User1 == userName && c.User2 == adminName)))
            {
                conversation = _contex.Conversations.Include(c => c.Messages).First(c => (c.User1 == adminName && c.User2 == userName) || (c.User1 == userName && c.User2 == adminName));
                conversation.SeenBy1 = false;
                conversation.SeenBy2 = false;
            }
            else
            {
                conversation = new Conversation
                {
                    User1 = userName,
                    User2 = adminName,
                    SeenBy1 = false,
                    SeenBy2 = false,
                    Messages = new List<Message>()
                };
                _contex.Users.Include(u => u.MessageBox.Conversations).Single(u => u.UserName == userName).MessageBox.Conversations.Add(conversation);
                _contex.Users.Include(u => u.MessageBox.Conversations).Single(u => u.UserName == adminName).MessageBox.Conversations.Add(conversation);
            }

            var message = new Message
            {
                Receiver = userName,
                Sender = adminName,
                MessageDate = DateTime.Now,
                Contents = msg
            };

            conversation.Messages.Add(message);
            _contex.SaveChanges();
        }
    }
}
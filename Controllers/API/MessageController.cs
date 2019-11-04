using Appocal.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Appocal.Controllers.API
{
    public class MessageController : ApiController
    {
        private ApplicationDbContext _contex;

        public MessageController()
        {
            _contex = new ApplicationDbContext();
        }

        [HttpGet]
        [Authorize]
        public IHttpActionResult GetNumberOfUnread()
        {
            var numberOfUnread = 0;
            var userId = HttpContext.Current.User.Identity.GetUserId();
            var userName = _contex.Users.Single(u => u.Id == userId).UserName;
            var conversations = _contex.Users.Include(u => u.MessageBox.Conversations).Single(u => u.Id == userId).MessageBox.Conversations;

            foreach (var conversation in conversations)
            {
                if (conversation.User1 == userName)
                {
                    if (conversation.SeenBy1 == false)
                        numberOfUnread++;
                }
                else if (conversation.User2 == userName)
                {
                    if (conversation.SeenBy2 == false)
                        numberOfUnread++;
                }
            }
            return Ok(numberOfUnread);
        }

        [HttpPatch]
        [Authorize]
        public IHttpActionResult SetSeen(string id)
        {
            var reciever = id; // id tutaj jako userName
            var userName = HttpContext.Current.User.Identity.GetUserName();
            var conversation = _contex.Conversations.Single(c => (c.User1 == userName && c.User2 == reciever) || (c.User2 == userName && c.User1 == reciever));
            if (conversation.User1 == userName)
                conversation.SeenBy1 = true;
            else
                conversation.SeenBy2 = true;

            _contex.SaveChanges();
            return Ok();
        }
    }
}
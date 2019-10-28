using Appocal.Models;
using Appocal.ViewModels;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace Appocal.Controllers
{
    public class UserController : Controller
    {
        private ApplicationDbContext _contex;

        public UserController()
        {
            _contex = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _contex.Dispose();
        }

        [Authorize]
        public ActionResult NewMessageForm(string name)
        {
            if (_contex.Users.Any(u => u.UserName == name))
            {
                var userId = HttpContext.User.Identity.GetUserId();
                var model = new MessageViewModel
                {
                    ReciverName = name,
                    SenderName = _contex.Users.Single(u => u.Id == userId).UserName
                };
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
using System;
using System.Web.Mvc;

namespace Appocal.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }


        public ActionResult Index()
        {
            if (User.IsInRole("Business"))
            {
                return RedirectToAction("Index", "Calendar");
            }
            else if (User.IsInRole("Individual"))
            {
                return RedirectToAction("BusinessList", "User");
            }
            else
            {
                return View();
            }

        }
    }
}
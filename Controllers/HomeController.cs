﻿using System.Web.Mvc;

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
            return View();
        }
    }
}
using Appocal.Models;
using Appocal.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Appocal.Controllers
{
    public class BusinessSiteController : Controller
    {
        private ApplicationDbContext _contex;
        public BusinessSiteController()
        {
            _contex = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _contex.Dispose();
        }

        // GET: BusinessSite
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Show(string name)
        {
            var business = _contex.Users.SingleOrDefault(u => u.UserName == name);

            if (business == null)
                return RedirectToAction("Index", "Home");

            var businessId = business.Id;
            var model = getBusinessDescriptionViewModel(businessId);
            return View(model);
        }

        [Authorize(Roles = "Business")]
        public ActionResult Settings()
        {
            var userId = HttpContext.User.Identity.GetUserId();
            var model = getBusinessDescriptionViewModel(userId);
            return View(model);
        }


        [Authorize(Roles = "Business")]
        public ActionResult ChangeSettings(BusinessDescriptionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Settings", model);
            }
            var userId = HttpContext.User.Identity.GetUserId();
            var user = _contex.Users.Include(u => u.Business.BusinessPage.PageContent).Single(u => u.Id == userId);
            user.Business.BusinessPage.PageContent.Title = model.Title;
            user.Business.BusinessPage.PageContent.Content = model.Content;
            if (_contex.SaveChanges() > 0)
            {
                ViewBag.SuccessMessage = "Pomyślnie zapisano zmiany";
                return View("Settings", model);
            }
            else
            {
                ViewBag.SuccessMessage = "Coś poszło nie tak, zmiany nie zostały zapisane";
                return View("Settings", getBusinessDescriptionViewModel(userId));
            }
        }

        private BusinessDescriptionViewModel getBusinessDescriptionViewModel(string userId)
        {
            var model = new BusinessDescriptionViewModel(); 
            var user = _contex.Users.Include(u => u.Business.BusinessPage.PageContent).Single(u => u.Id == userId);
            model.Name = user.Business.Name;
            model.Title = user.Business.BusinessPage.PageContent.Title;
            model.Content = user.Business.BusinessPage.PageContent.Content;
            return model;
        }
    }
}
using Appocal.Models;
using Appocal.ViewModels;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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
            var model = GetBusinessDescriptionViewModel(businessId);

            var userId = HttpContext.User.Identity.GetUserId();
            if (userId == businessId)
                return View("BusinessSiteOwner", model);
            else
                return View(model);
        }

        [Authorize(Roles = "Business")]
        public ActionResult Services()
        {
            var userId = HttpContext.User.Identity.GetUserId();
            var model = GetBusinessServicesViewModel(userId);
            return View(model);
        }

        [Authorize(Roles = "Business")]
        public ActionResult AddService(BusinessServicesViewModel model)
        {
            var userId = HttpContext.User.Identity.GetUserId();
            BusinessServicesViewModel modelToReturn;
            if (!ModelState.IsValid)
            {
                modelToReturn = GetBusinessServicesViewModel(userId);
                modelToReturn.NewService = model.NewService;
                return View("Services", modelToReturn);
            }
            var user = _contex.Users.Include(u => u.Business.Services).Single(u => u.Id == userId);
            user.Business.Services.Add(new Service { Name = model.NewService.Name, Duration = model.NewService.Duration, Active = true });

            if (_contex.SaveChanges() > 0)
            {
                modelToReturn = GetBusinessServicesViewModel(userId);
                ViewBag.SuccessMessage = "Pomyślnie dodano nową usługę.";
                return View("Services", modelToReturn);
            }
            else
            {
                modelToReturn = GetBusinessServicesViewModel(userId);
                ViewBag.SuccessMessage = "Coś poszło nie tak, usługa nie została dodana.";
                return View("Services", modelToReturn);
            }
        }

        [Authorize(Roles = "Business")]
        public ActionResult EditService(ServiceViewModel model)
        {
            var userId = HttpContext.User.Identity.GetUserId();
            BusinessServicesViewModel modelToReturn;

            if (!ModelState.IsValid)
            {
                modelToReturn = GetBusinessServicesViewModel(userId);
                return View("Services", modelToReturn);
            }
            var user = _contex.Users.Include(u => u.Business.Services).Single(u => u.Id == userId);
            var service = user.Business.Services.SingleOrDefault(s => s.Id == model.Id);

            service.Duration = model.Duration;
            service.Name = model.Name;

            if (_contex.SaveChanges() > 0)
            {
                modelToReturn = GetBusinessServicesViewModel(userId);
                ViewBag.SuccessMessage = "Pomyślnie zapisano zmiany";
                return View("Services", modelToReturn);
            }
            else
            {
                modelToReturn = GetBusinessServicesViewModel(userId);
                ViewBag.SuccessMessage = "Coś poszło nie tak, zmiany nie zostały zapisane";
                return View("Services", modelToReturn);
            }
        }
        [Authorize(Roles = "Business")]
        public ActionResult DeleteService(ServiceViewModel model)
        {
            var userId = HttpContext.User.Identity.GetUserId();
            BusinessServicesViewModel modelToReturn;

            if (!ModelState.IsValid)
            {
                modelToReturn = GetBusinessServicesViewModel(userId);
                return View("Services", modelToReturn);
            }
            var user = _contex.Users.Include(u => u.Business.Services).Single(u => u.Id == userId);
            var service = user.Business.Services.SingleOrDefault(s => s.Id == model.Id);

            service.Active = false;

            if (_contex.SaveChanges() > 0)
            {
                modelToReturn = GetBusinessServicesViewModel(userId);
                ViewBag.SuccessMessage = "Pomyślnie usunięto usługę";
                return View("Services", modelToReturn);
            }
            else
            {
                modelToReturn = GetBusinessServicesViewModel(userId);
                ViewBag.SuccessMessage = "Coś poszło nie tak, usługa nie została usunięta";
                return View("Services", modelToReturn);
            }
        }

        [Authorize(Roles = "Business")]
        public ActionResult Settings()
        {
            var userId = HttpContext.User.Identity.GetUserId();
            var model = GetBusinessDescriptionViewModel(userId);
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
                return View("Settings", GetBusinessDescriptionViewModel(userId));
            }
        }

        private BusinessServicesViewModel GetBusinessServicesViewModel(string userId)
        {
            var services = _contex.Users.Include(u => u.Business.Services).SingleOrDefault(u => u.Id == userId).Business.Services.ToList();
            var model = new BusinessServicesViewModel();
            model.Services = new List<ServiceViewModel>();
            foreach (var service in services)
            {
                var singleService = new ServiceViewModel { Id= service.Id, Duration = service.Duration, Name = service.Name };
                if (service.Active == true)
                {
                    model.Services.Add(singleService);
                }
            }
            return model;
        }

        private BusinessDescriptionViewModel GetBusinessDescriptionViewModel(string userId)
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
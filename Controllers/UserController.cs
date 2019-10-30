using Appocal.Models;
using Appocal.ViewModels;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;

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

        [Authorize(Roles = "Individual")]
        public ActionResult BusinessList()
        {
            var model = new BusinessListViewModel
            {
                Businesses = _contex.Businesses.Where(b => b.Public == true).ToList()
            };

            return View(model);
        }

        [Authorize(Roles = "Individual")]
        public ActionResult Schedule()
        {
            var userId = HttpContext.User.Identity.GetUserId();
            var model = new ScheduleViewModel();
            var Appointments = _contex.Users.Include(u => u.Schedule.Appointments).Single(u => u.Id == userId).Schedule.Appointments;

            foreach(var appointment in Appointments)
            {
                var viewAppo = new AppointmentViewModel
                {
                    Id = appointment.Id,
                    BusinessName = appointment.Business_Name,
                    Confirmed = appointment.IsConfirmed,
                    Date = appointment.AppointmentDate,
                    Duration = appointment.Duration,
                    Service = _contex.Businesses.Include(b=>b.Services)
                                                .Single(b=>b.Name==appointment.Business_Name).Services
                                                .Single(s=>s.Id== appointment.Service_id).Name
                };
                model.Appointments.Add(viewAppo);
            }
            model.Appointments = model.Appointments.OrderBy(a => a.Date).ToList();
            return View(model);
        }


    }
}
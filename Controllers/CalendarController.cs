using Appocal.Models;
using Appocal.ViewModels;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Appocal.Controllers
{
    public class CalendarController : Controller
    {
        private ApplicationDbContext _contex;

        public CalendarController()
        {
            _contex = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _contex.Dispose();
        }

        public ActionResult Index()
        {
            var userId = HttpContext.User.Identity.GetUserId();
            List<Appointment> appointmentsInDisplayedMonth = _contex.Users.Include(u => u.Business.Schedule.Appointments)
                                                                          .Single(u => u.Id == userId).Business.Schedule.Appointments.Where(a => a.AppointmentDate.Month == 9).ToList();
            CalendarViewModel model = new CalendarViewModel(9, 2019, appointmentsInDisplayedMonth);
            return View("Calendar", model);
        }

        public ActionResult AddNewTime()
        {
            WorkTimeViewModel model = new WorkTimeViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult SaveWorkTime(WorkTimeViewModel WTD)
        {
            var userId = HttpContext.User.Identity.GetUserId();
            Appointment WorkTime = new Appointment();
            WorkTime.AppointmentDate = WTD.AppointmentDate;
            var duration = (WTD.AppointmentEndDate.Hour - WTD.AppointmentDate.Hour) * 60 + WTD.AppointmentEndDate.Minute - WTD.AppointmentDate.Minute;

            if (duration < 10)
            {
                ModelState.AddModelError("Duration", "Czas pracy jest za krótki.");
                return View("AddNewTime", WTD);
            }

            var user = _contex.Users.Include(u => u.Business.Schedule.Appointments).Single(u => u.Id == userId);

            WorkTime.Duration = duration;
            WorkTime.Available = true;

            user.Business.Schedule.Appointments.Add(WorkTime);

            for (int i = 0; i < WTD.TimePeriodToRepeat - 1; i++)
            {
                WorkTime = WorkTime.Clone();
                WorkTime.AppointmentDate = WorkTime.AppointmentDate.AddDays(1);
                switch (WTD.Repeat)
                {
                    case 0:
                        i = (int)WTD.TimePeriodToRepeat;
                        break;

                    case 1:
                        user.Business.Schedule.Appointments.Add(WorkTime);
                        break;

                    case 2:
                        if ((int)WorkTime.AppointmentDate.DayOfWeek >= 1 && (int)WorkTime.AppointmentDate.DayOfWeek <= 5)
                            user.Business.Schedule.Appointments.Add(WorkTime);
                        break;

                    case 3:
                        if ((int)WorkTime.AppointmentDate.DayOfWeek == 0 || (int)WorkTime.AppointmentDate.DayOfWeek == 6)
                            user.Business.Schedule.Appointments.Add(WorkTime);
                        break;

                    case 4:
                        if ((int)WorkTime.AppointmentDate.DayOfWeek == 6)
                            user.Business.Schedule.Appointments.Add(WorkTime);
                        break;

                    case 5:
                        if ((int)WorkTime.AppointmentDate.DayOfWeek == 0)
                            user.Business.Schedule.Appointments.Add(WorkTime);
                        break;

                    default:
                        break;
                }
            }
            if (_contex.SaveChanges() > 0)
                ViewBag.SuccessMessage = "Pomyślnie dodano nowy czas pracy";
            else
                ViewBag.SuccessMessage = "Coś poszło nie tak, czas pracy nie został dodany";
            ModelState.Clear();
            return View("AddNewTime", new WorkTimeViewModel());
        }
    }
}
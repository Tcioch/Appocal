using Appocal.Models;
using Appocal.Models.HelperModels;
using Appocal.ViewModels;
using Microsoft.AspNet.Identity;
using System;
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
            return View("Calendar");
        }

        [HttpPost]
        public ActionResult SetAppointment(AppointmentDataApi appoData)
        {
            var userId = HttpContext.User.Identity.GetUserId();
            var business = _contex.Businesses.Include(b => b.Schedule.Appointments)
                                    .SingleOrDefault(b => b.Name == appoData.BusinessName);
            var businessAppointments = business.Schedule.Appointments;
            var workTime = businessAppointments.FirstOrDefault(a => a.Id == int.Parse(appoData.TimeOfMeetingWithId.Split('_').Last()));

            DateTime newAppointmentDate = new DateTime(int.Parse(appoData.DateOfMeeting.ToString().Substring(0, 4)),
                                                       int.Parse(appoData.DateOfMeeting.ToString().Substring(4, 2)),
                                                       int.Parse(appoData.DateOfMeeting.ToString().Substring(6, 2)),
                                                       int.Parse(appoData.TimeOfMeetingWithId.Substring(0, 2)),
                                                       int.Parse(appoData.TimeOfMeetingWithId.Substring(2, 2)), 0);
            Appointment newAppointment = new Appointment
            {
                AppointmentDate = newAppointmentDate,
                Client_Id = userId,
                Business_Name = business.Name,
                Duration = _contex.Services.Single(s => s.Id == appoData.ServiceId).Duration,
                Available = false,
                IsConfirmed = false,
                Service_id = appoData.ServiceId
            };

            int restDurationOfOldAppointment = (int)((newAppointmentDate.Ticks - workTime.AppointmentDate.Ticks) / 600000000L);

            int totalOldDuration = workTime.Duration;

            if (restDurationOfOldAppointment > 0 && totalOldDuration > (restDurationOfOldAppointment + newAppointment.Duration))
            {
                workTime.Duration = restDurationOfOldAppointment;
                var newRestAppointment = new Appointment
                {
                    Duration = totalOldDuration - restDurationOfOldAppointment - newAppointment.Duration,
                    Available = true,
                    IsConfirmed = false,
                    AppointmentDate = newAppointmentDate.AddMinutes(newAppointment.Duration)
                };
                businessAppointments.Add(newAppointment);
                businessAppointments.Add(newRestAppointment);
            }
            else if (restDurationOfOldAppointment > 0 && totalOldDuration == (restDurationOfOldAppointment + newAppointment.Duration))
            {
                workTime.Duration = restDurationOfOldAppointment;
                businessAppointments.Add(newAppointment);
            }
            else if (restDurationOfOldAppointment == 0 && totalOldDuration > (restDurationOfOldAppointment + newAppointment.Duration))
            {
                businessAppointments.Remove(workTime);
                var newRestAppointment = new Appointment
                {
                    Duration = totalOldDuration - restDurationOfOldAppointment - newAppointment.Duration,
                    Available = true,
                    IsConfirmed = false,
                    AppointmentDate = newAppointmentDate.AddMinutes(newAppointment.Duration)
                };
                businessAppointments.Add(newAppointment);
                businessAppointments.Add(newRestAppointment);
            }
            else
            {
                businessAppointments.Remove(workTime);
                businessAppointments.Add(newAppointment);
            }

            _contex.Users.Include(u => u.Schedule.Appointments).Single(u => u.Id == userId).Schedule.Appointments.Add(newAppointment);

            if (_contex.SaveChanges() > 0)
                TempData["message"] = "Spotkanie zostało pomyślnie umówione!";
            else
                TempData["message"] = "Niestety coś poszło nie tak. Spotkanie nie zostało umówione";
            ModelState.Clear();

            return RedirectToAction("Show", "BusinessSite", new { name = appoData.BusinessName });
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
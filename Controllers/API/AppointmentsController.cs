using Appocal.Models;
using Appocal.Models.HelperModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace Appocal.Controllers.API
{
    public class AppointmentsController : ApiController
    {
        private ApplicationDbContext _contex;

        public AppointmentsController()
        {
            _contex = new ApplicationDbContext();
        }

        //Get /api/calendar/i
        public IHttpActionResult GetAppointments(string date)
        {
            var userId = User.Identity.GetUserId();
            List<Appointment> appointmentsQuery = _contex.Users.Include(u => u.Business.Schedule.Appointments).Single(u => u.Id == userId).Business.Schedule.Appointments;
            appointmentsQuery = appointmentsQuery.Where(a => a.AppointmentDate.ToString("yyyyMMdd") == date).ToList();
            appointmentsQuery = appointmentsQuery.OrderBy(a => a.AppointmentDate).ToList();
            foreach(var app in appointmentsQuery)
            {
                app.Schedule = null;
            }
            return Ok(appointmentsQuery);
        }

        [HttpPost]
        public IHttpActionResult GetAvailableHours(AppointmentRequest appointmentRequest)
        {
            DateTime appoDate = new DateTime(int.Parse(appointmentRequest.Date.Substring(0, 4)),
                                    int.Parse(appointmentRequest.Date.Substring(4, 2)),
                                    int.Parse(appointmentRequest.Date.Substring(6, 2)));

            var availableAppointments = _contex.Businesses.Include(b => b.Schedule.Appointments)
                                                .SingleOrDefault(b => b.Name == appointmentRequest.BusinessName)
                                                .Schedule.Appointments
                                                .Where(a => a.AppointmentDate.ToString("yyyyMMdd") == appoDate.ToString("yyyyMMdd")
                                                && a.AppointmentDate.Day == appoDate.Day && a.Available == true).ToList();

            var services = _contex.Businesses.Include(b => b.Services)
                                            .SingleOrDefault(b => b.Name == appointmentRequest.BusinessName).Services;

            int shortestDuration = services.Min(s => s.Duration);
            int durationOfChosenService = services.SingleOrDefault(a => a.Id == appointmentRequest.ServiceId).Duration;

            List<string> availableHours = new List<string>();

            foreach (var appointment in availableAppointments)
            {
                int durationOfAvailableTime = appointment.Duration;
                DateTime currentHour = appointment.AppointmentDate;
                while (true)
                {
                    if (durationOfAvailableTime >= durationOfChosenService)
                    {
                        availableHours.Add(currentHour.ToString("HHmm") + "_" + appointment.Id.ToString());
                        durationOfAvailableTime -= shortestDuration;
                        currentHour = currentHour.AddMinutes(shortestDuration);
                    }
                    else break;
                }
            }
            availableHours.Sort();
            return Ok(availableHours);
        }
    }
}
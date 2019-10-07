using Appocal.Dtos;
using Appocal.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Appocal.Controllers.API
{
    public class CalendarController : ApiController
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

        [Authorize(Roles = "Business, Individual")]
        public IHttpActionResult GetDaysInMonthWithAppointments(int month, int year)
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            List<Appointment> appointmentsInDisplayedMonth = _contex.Users.Include(u => u.Business.Schedule.Appointments)
                                                                          .Single(u => u.Id == userId).Business.Schedule.Appointments
                                                                          .Where(a => a.AppointmentDate.Month == month && a.AppointmentDate.Year == year)
                                                                          .ToList();

            var daysInMonth = new List<CalendarDayDto>();
            foreach (var appointment in appointmentsInDisplayedMonth)
            {
                var calendarDay = new CalendarDayDto(appointment.AppointmentDate.Day, appointment.AppointmentDate.Month, appointment.AppointmentDate.Year, "empty");
                List<Appointment> singleFullDay = appointmentsInDisplayedMonth.Where(a => a.AppointmentDate.Day == appointment.AppointmentDate.Day).ToList();

                if (singleFullDay.Any(a => a.Available == false) && singleFullDay.Any(a => a.Available == true))
                    calendarDay.Status = "PartlyBooked";
                else if (singleFullDay.Any(a => a.Available == false) && !singleFullDay.Any(a => a.Available == true))
                    calendarDay.Status = "FullyBooked";
                else
                    calendarDay.Status = "Free";

                daysInMonth.Add(calendarDay);

                appointmentsInDisplayedMonth = appointmentsInDisplayedMonth.Except(singleFullDay).ToList();
            }
            return Ok(daysInMonth);
        }

        
    }
}
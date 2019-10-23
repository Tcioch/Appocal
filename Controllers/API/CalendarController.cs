using Appocal.Dtos;
using Appocal.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
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
        public IHttpActionResult GetDaysInMonthWithAppointments(int month, int year, string businessName = null)
        {
            string userId;
            if (businessName == null)
                userId = HttpContext.Current.User.Identity.GetUserId();
            else
            {
                var business = _contex.Users.SingleOrDefault(u => u.UserName == businessName);
                userId = business.Id;
            }

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

        [HttpPatch]
        [Authorize(Roles = "Business")]
        public IHttpActionResult ConfirmAppointment(int id)
        {

            var userId = HttpContext.Current.User.Identity.GetUserId();

            if (_contex.Users.Include(u => u.Business.Schedule.Appointments).SingleOrDefault(u => u.Id == userId).Business.Schedule.Appointments.Any(a => a.Id == id))
            {
                var appointmentToConfirm = _contex.Appointments.Single(a => a.Id == id);
                appointmentToConfirm.IsConfirmed = true;
                if (_contex.SaveChanges() > 0)
                {
                    return Ok(true);
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return NotFound();
            }
        }


        [HttpDelete]
        [Authorize(Roles = "Business")]
        public IHttpActionResult DeleteAppointment(int id)
        {
            
            var userId = HttpContext.Current.User.Identity.GetUserId();

            if (_contex.Users.Include(u => u.Business.Schedule.Appointments).SingleOrDefault(u => u.Id == userId).Business.Schedule.Appointments.Any(a => a.Id == id))
            {
                var appointmentToDelete = _contex.Appointments.Single(a => a.Id == id);
                _contex.Appointments.Remove(appointmentToDelete);
                if (_contex.SaveChanges() > 0)
                {
                    return Ok(true);
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return NotFound();
            }
        }
    }
}
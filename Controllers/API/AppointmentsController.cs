using Appocal.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Security;

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
            return Ok(appointmentsQuery);
        }

    }
}

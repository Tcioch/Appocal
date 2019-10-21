using Appocal.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace Appocal.Controllers.API
{
    public class BusinessController : ApiController
    {
        private ApplicationDbContext _contex;
        public BusinessController()
        {
            _contex = new ApplicationDbContext();
        }
        //Get /api/calendar/i
        public IHttpActionResult GetServices(string name)
        {
            var services = _contex.Users.Include(u => u.Business.Services).Single(u => u.UserName == name).Business.Services;
            return Ok(services);
        }
    }
}
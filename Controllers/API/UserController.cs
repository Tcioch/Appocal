using Appocal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Appocal.Controllers.API
{
    public class UserController : ApiController
    {
        private ApplicationDbContext _contex;
        public UserController()
        {
            _contex = new ApplicationDbContext();
        }
        public IHttpActionResult getUserName(string id)
        {
            var user = _contex.Users.SingleOrDefault(u => u.Id == id);
            return Ok(user.UserName);
        }

    }
}

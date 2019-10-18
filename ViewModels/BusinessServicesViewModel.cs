using Appocal.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Appocal.ViewModels
{
    public class BusinessServicesViewModel
    {
        public List<Service> Services { get; set; }

        public BusinessServicesViewModel(string userId)
        {
            var _contex = new ApplicationDbContext();
            Services = _contex.Users.Include(u => u.Business.Services).SingleOrDefault(u => u.Id == userId).Business.Services.ToList();
        }
    }
}
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
        public List<ServiceViewModel> Services { get; set; }
        public ServiceViewModel NewService { get; set; }
    }
}
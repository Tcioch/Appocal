using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Services.Description;

namespace Appocal.Models
{
    public class Service
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public int Duration { get; set; }
        public bool Active { get; set; }
    }
}
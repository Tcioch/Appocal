using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appocal.Models.HelperModels
{
    public class AppointmentRequest
    {
        public int ServiceId { get; set; }
        public string BusinessName { get; set; }
        public string Date { get; set; }
    }
}
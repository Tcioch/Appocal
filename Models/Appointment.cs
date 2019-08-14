using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appocal.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int Duration { get; set; }
        public bool? Available { get; set; }
        public ApplicationUser Client{ get; set; }
        public bool IsConfirmed { get; set; }
    }
}
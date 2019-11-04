using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appocal.Models
{
    public class Schedule
    {
        public int Id { get; set; }
        public List<Appointment> Appointments{ get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Appocal.Models
{
    public class Appointment : ICloneable
    {
        public int Id { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int Duration { get; set; }
        public bool? Available { get; set; }
        public string Client_Id{ get; set; }
        public bool IsConfirmed { get; set; }
        public int? Service_id { get; set; }
        public Appointment Clone()
        {
            return (Appointment)this.MemberwiseClone();
        }

        object ICloneable.Clone()
        {
            return Clone();
        }
    }
}
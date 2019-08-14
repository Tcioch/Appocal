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

        public void AddAppointment(Appointment appointment)
        {
            Appointments.Add(appointment);
            //Appointments.Sort();
        }

        public void RemoveAppointment(Appointment appointment)
        {
            Appointments.Remove(appointment);
            //Appointments.Sort();
        }

        public void RemoveAppointment(int appointmentId)
        {
            Appointments.Remove(Appointments.SingleOrDefault(a => a.Id == appointmentId));
        }
    }
}
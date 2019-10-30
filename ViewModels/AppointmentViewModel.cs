using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace Appocal.ViewModels
{
    public class AppointmentViewModel
    {
        public int Id { get; set; }
        public string BusinessName { get; set; }
        public int Duration { get; set; }
        public string Service { get; set; }
        public DateTime Date { get; set; }
        public bool Confirmed { get; set; }

    }
}
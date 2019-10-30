using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Web;

namespace Appocal.ViewModels
{
    public class ScheduleViewModel
    {
        public List<AppointmentViewModel> Appointments { get; set; }
        public ScheduleViewModel()
        {
            Appointments = new List<AppointmentViewModel>();
        }
    }
}
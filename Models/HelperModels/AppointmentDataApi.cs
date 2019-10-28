using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appocal.Models.HelperModels
{
    public class AppointmentDataApi
    {
        public string BusinessName { get; set; }
        public int ServiceId { get; set; }
        public int DateOfMeeting { get; set; }
        public string TimeOfMeetingWithId { get; set; }

    }
}
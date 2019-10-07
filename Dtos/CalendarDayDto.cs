using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appocal.Dtos
{
    public class CalendarDayDto
    {
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public string Status { get; set; }
        public CalendarDayDto(int day, int month, int year, string status)
        {
            Day = day;
            Month = month;
            Year = year;
            if (status == "FullyBooked" || status == "PartlyBooked" || status == "Free")
                Status = status;
            else
                Status = "Free";
        }
    }
}
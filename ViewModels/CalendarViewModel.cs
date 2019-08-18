using Microsoft.Ajax.Utilities;
using System;

namespace Appocal.ViewModels
{
    public class CalendarViewModel
    {
        public int FirstDayOfMonth { get; set; }
        public int NumberOfRows { get; set; }
        public int NumberOfDays { get; set; }

        public CalendarViewModel(int month, int year)
        {
            DateTime date = new DateTime(year, month , 1);
            FirstDayOfMonth = (int)date.DayOfWeek;
            NumberOfDays = DateTime.DaysInMonth(year, month);
            NumberOfRows = CalculateNumberOfRows();
        }

        private int CalculateNumberOfRows()
        {
            if (FirstDayOfMonth == 0)
                FirstDayOfMonth = 7;

            int daysInCalendarWithEmptyFields = NumberOfDays + (FirstDayOfMonth - 1);
            return (int)Math.Ceiling((daysInCalendarWithEmptyFields / 7m));
        }

        public string DrawCalendar()
        {
            string ReturnString = "";
            ReturnString += "<table class='table table-bordered'><tr>" +
                "<th scope='col'>Pon</th>" +
                "<th scope='col'>Wt</th>" +
                "<th scope='col'>Śr</th>" +
                "<th scope='col'>Czw</th>" +
                "<th scope='col'>Pt</th>" +
                "<th scope='col'>Sb</th>" +
                "<th scope='col'>Nd</th>" +
                "</tr>";
            int dayInCalendar = 1;
            int rowInCalendar = 1;
            while (rowInCalendar <= NumberOfRows)
            {
                ReturnString += "<tr>";
                if (rowInCalendar == 1)
                {
                    for (int i = 1; i <= 7; i++)
                    {
                        if (i < FirstDayOfMonth)
                            ReturnString += "<th></th>";
                        else
                        {
                            ReturnString += $"<th>{dayInCalendar}</th>";
                            dayInCalendar++;
                        }
                    }
                }
                else
                {
                    for (int i = 1; i <= 7; i++)
                    {
                        ReturnString += $"<th>{dayInCalendar}</th>";
                        dayInCalendar++;
                        if (dayInCalendar > NumberOfDays) break;
                    }
                }
                ReturnString += "</tr>";
                rowInCalendar++;
            }
            ReturnString += "</table>";
            return ReturnString;
        }
    }
}
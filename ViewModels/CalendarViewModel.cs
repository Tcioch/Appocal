using Appocal.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Appocal.ViewModels
{
    public class CalendarViewModel
    {
        public DateTime firstDay { get; set; }
        public int FirstDayOfMonth { get; set; }
        public int NumberOfRows { get; set; }
        public int NumberOfDays { get; set; }
        public List<Appointment> Appointments { get; set; }

        public CalendarViewModel(int month, int year, List<Appointment> appointments)
        {
            DateTime date = new DateTime(year, month, 1);
            firstDay = date;
            FirstDayOfMonth = (int)date.DayOfWeek;
            NumberOfDays = DateTime.DaysInMonth(year, month);
            NumberOfRows = CalculateNumberOfRows();
            Appointments = appointments;
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
                        string appDate = firstDay.Year.ToString() + firstDay.Month.ToString().PadLeft(2, '0') + dayInCalendar.ToString().PadLeft(2, '0');
                        if (i < FirstDayOfMonth)
                            ReturnString += "<th></th>";
                        else
                        {
                            string htmlInCell;
                            if (Appointments.Any(a => a.AppointmentDate.Day == dayInCalendar))
                            {
                                if (Appointments.Any(a => a.Available == false) && Appointments.Any(a => a.Available == true))
                                    htmlInCell = $"<a href='#' class='btn btn-warning w-100' appDate='{appDate}'>{dayInCalendar}</a>";
                                else if (Appointments.Any(a => a.Available == false) && !Appointments.Any(a => a.Available == true))
                                    htmlInCell = $"<a href='#' class='btn btn-danger w-100' appDate='{appDate}'>{dayInCalendar}</a>";
                                else
                                    htmlInCell = $"<a href='#' class='btn btn-success w-100' appDate='{appDate}'>{dayInCalendar}</a>";
                            }
                            else
                            {
                                htmlInCell = $"<a class='btn btn-secondary w-100 disabled'>{dayInCalendar}</a>";
                            }
                            ReturnString += $"<td>{htmlInCell}</td>";
                            dayInCalendar++;
                        }
                    }
                }
                else
                {
                    for (int i = 1; i <= 7; i++)
                    {
                        string appDate = firstDay.Year.ToString() + firstDay.Month.ToString().PadLeft(2, '0') + dayInCalendar.ToString().PadLeft(2, '0');
                        string htmlInCell;
                        if (Appointments.Any(a => a.AppointmentDate.Day == dayInCalendar))
                        {
                            if (Appointments.Any(a => a.Available == false) && Appointments.Any(a => a.Available == true))
                                htmlInCell = $"<a href='#' class='btn btn-warning w-100' appDate='{appDate}'>{dayInCalendar}</a>";
                            else if (Appointments.Any(a => a.Available == false) && !Appointments.Any(a => a.Available == true))
                                htmlInCell = $"<a href='#' class='btn btn-danger w-100' appDate='{appDate}'>{dayInCalendar}</a>";
                            else
                                htmlInCell = $"<a href='#' class='btn btn-success w-100' appDate='{appDate}'>{dayInCalendar}</a>";
                        }
                        else
                        {
                            htmlInCell = $"<a class='btn btn-secondary w-100 disabled'>{dayInCalendar}</a>";
                        }
                        ReturnString += $"<td>{htmlInCell}</td>";
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
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Appocal.ViewModels
{
    public class WorkTimeViewModel
    {
        public int? Id { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Rozpoczęcie pracy")]
        public DateTime AppointmentDate { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [Display(Name = "Godzina zakończenia pracy")]
        public DateTime AppointmentEndDate { get; set; }

        public uint Repeat { get; set; }

        public uint TimePeriodToRepeat { get; set; }

        public List<RepeatOption> RepeatOptions { get; }

        public List<TimePeriodOption> TimePeriodOptions { get; }

        public WorkTimeViewModel()
        {
            RepeatOptions = new List<RepeatOption> {new RepeatOption { Value = 0, Desc = "Nigdy" },
                                                        new RepeatOption { Value = 1, Desc = "Codziennie" },
                                                        new RepeatOption { Value = 2, Desc = "W dni robocze" },
                                                        new RepeatOption { Value = 3, Desc = "W weekendy" },
                                                        new RepeatOption { Value = 4, Desc = "W soboty" },
                                                        new RepeatOption { Value = 5, Desc = "W niedziele" } };

            TimePeriodOptions = new List<TimePeriodOption> {new TimePeriodOption { Value = 7, Desc = "tydzień" },
                                                        new TimePeriodOption { Value = 14, Desc = "dwa tygodnie" },
                                                        new TimePeriodOption { Value = 30, Desc = "miesiąc" },
                                                        new TimePeriodOption { Value = 60, Desc = "dwa miesiące" },
                                                        new TimePeriodOption { Value = 183, Desc = "pół roku" },
                                                        new TimePeriodOption { Value = 365, Desc = "rok" } };
        }

        public class RepeatOption
        {
            public uint Value { get; set; }
            public string Desc { get; set; }
        }

        public class TimePeriodOption
        {
            public uint Value { get; set; }
            public string Desc { get; set; }
        }
    }
}
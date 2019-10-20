using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Appocal.ViewModels
{
    public class ServiceViewModel
    {
        [Required]
        [Display(Name = "Nazwa usługi")]
        public String Name { get; set; }
        [Required]
        [Display(Name = "Czas trwania")]
        [Range(1, 1440, ErrorMessage = "Podaj wartość od {0} do {1})")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Wprowadź liczbę")]
        public int Duration { get; set; }
    }
}
using Appocal.Models;
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
        public int Id { get; set; }
        [Required]
        [Display(Name = "Nazwa usługi")]
        public String Name { get; set; }
        [Required]
        [Display(Name = "Czas trwania")]
        [Range(5, 1440, ErrorMessage = "Podaj czas trwania od {1} do {2}")]
        [DivisibleBy5(ErrorMessage = "Czas trwania musi być podzielny przez 5")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Wprowadź liczbę")]
        public int Duration { get; set; }
    }
}
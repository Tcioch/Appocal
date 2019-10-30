using Appocal.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Appocal.ViewModels
{
    public class BusinessDescriptionViewModel
    {
        public string Name { get; set; }
        [Required]
        [Display(Name = "Tytuł")]
        [StringLength(100, ErrorMessage = "{0} musi zawierać od {2} do {1} znaków.", MinimumLength = 6)]
        public string Title { get; set; }
        [Required]
        [Display(Name = "Opis")]
        [AllowHtml]
        public string Content { get; set; }
        [Required]
        public bool Public { get; set; }
        [Required]
        [Display(Name = "Krótki opis")]
        [StringLength(120, ErrorMessage = "{0} musi zawierać od {2} do {1} znaków.", MinimumLength = 25)]
        public string ShortDescription { get; set; }
        public List<Review> Reviews { get; set; }
    }
}
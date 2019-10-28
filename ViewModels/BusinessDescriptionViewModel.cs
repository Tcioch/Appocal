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
        [AllowHtmlAttribute]
        //[StringLength(10000, ErrorMessage = "{0} musi zawierać do {1} znaków.")]
        public string Content { get; set; }
        public List<Review> Reviews { get; set; }
    }
}
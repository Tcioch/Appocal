using Appocal.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Appocal.ViewModels
{
    public class ReviewViewModel
    {
        [Required]
        public string BusinessName { get; set; }
        [Required]
        [DivisibleBy0dot5]
        [Range(1,5)]
        public float Rating { get; set; }
        [Required]
        public string Review { get; set; }

    }
}
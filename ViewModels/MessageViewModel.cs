using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Web;

namespace Appocal.ViewModels
{
    public class MessageViewModel
    {
        public string SenderName { get; set; }
        public string ReciverName { get; set; }
        [Required]
        [Display(Name = "Wiadomość")]
        public string Message { get; set; }

    }
}
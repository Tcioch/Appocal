using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Appocal.ViewModels
{
    public class ConversationViewModel
    {
        public List<MessageViewModel> Messages { get; set; }
        public bool SeenBy1 { get; set; }
        public bool SeenBy2 { get; set; }
        public string User1 { get; set; }
        public string User2 { get; set; }
    }
}
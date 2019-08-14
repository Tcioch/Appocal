using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appocal.Models
{
    public class Conversation
    {
        public int Id { get; set; }
        public ApplicationUser User1 { get; set; }
        public ApplicationUser User2 { get; set; }
        public List<Message> Messages { get; set; }
    }
}
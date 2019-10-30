using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appocal.Models
{
    public class Conversation
    {
        public int Id { get; set; }
        public List<Message> Messages { get; set; }
        public bool SeenBy1 { get; set; }
        public bool SeenBy2 { get; set; }
        public string User1 { get; set; }
        public string User2 { get; set; }
        public ICollection<MessageBox> MessageBox { get; set; }
    }
}
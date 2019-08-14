using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appocal.Models
{
    public class Message
    {
        public int Id { get; set; }
        public DateTime MessageDate { get; set; }
        public String Contents { get; set; }
    }
}
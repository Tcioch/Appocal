using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appocal.Models
{
    public class Review
    {
        public int Id{ get; set; }
        public DateTime ReviewDate { get; set; }
        public String Client_Id { get; set; }
        public float Rating { get; set; }
        public string Contents { get; set; }
    }
}
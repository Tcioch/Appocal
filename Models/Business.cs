using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appocal.Models
{
    public class Business
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Schedule Schedule { get; set; }
        public BusinessPage BusinessPage{ get; set; }
        public List<Review> Reviews{ get; set; }
    }
}
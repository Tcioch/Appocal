using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appocal.Models
{
    public class MessageBox
    {
        public int Id { get; set; }
        public List<Conversation> Conversations { get; set; }
    }
}
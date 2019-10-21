using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Appocal.Models
{
    public class DivisibleBy5 : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if ((int)value % 5 == 0)
                return true;
            else
                return false;
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace kdh.Utils
{
    public class ValidAgeValidator : ValidationAttribute
    {
        public object CheckDate { get; set; }

        public override bool IsValid(object date)
        {
            if (date != null)
            {
                DateTime now = (DateTime)date;
                if (now <= (DateTime)CheckDate)
                {
                    return true;
                }
                return false;
            }
            else if (date == null)
            {
                return true;
            }
            return false;
        }
    }
}
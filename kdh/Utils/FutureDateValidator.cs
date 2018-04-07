using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace kdh.Utils
{
    public class FutureDateValidator : ValidationAttribute
    {
        public override bool IsValid(object date)
        {

            if (date != null)
            {
                DateTime d = (DateTime)date;
                return d >= DateTime.Now;
            }
            else if (date == null)
            {
                return true;
            }
            return false;
        }
    }
}
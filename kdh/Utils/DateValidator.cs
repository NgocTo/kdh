using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PatientPortal_IS_0307.Classes
{
    public class DateValidator : ValidationAttribute
    {
        public override bool IsValid(object date)
        {

            if (date != null)
            {
                DateTime d = (DateTime)date;
                return d < DateTime.Now;
            }
            else if (date == null)
            {
                return true;
            }
            return false;
        }
    }
}
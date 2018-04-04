using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace kdh.Utils
{
    public class DateValidator : ValidationAttribute
    {
        /// <summary>
        /// Validate if the date passed to the paramater is day before today (past date). 
        /// Return true if the passed date is past or null.
        /// Return false if the passed date is future.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
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
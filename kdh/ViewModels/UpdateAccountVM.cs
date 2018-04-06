using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace kdh.ViewModels
{
    public class UpdateAccountVM
    {

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [StringLength(100, ErrorMessage = "Field must be less than 100 characters.")]
        [Remote("IsAvailableEmail", "Patient", HttpMethod = "POST", ErrorMessage = "Email address is already in use.")]
        public string Email { get; set; }


    }
}
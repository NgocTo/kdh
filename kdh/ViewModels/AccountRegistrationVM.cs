using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace kdh.ViewModels
{
    public class AccountRegistrationVM
    {
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(20, ErrorMessage = "Must be between 6 and 20 characters", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "*Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [StringLength(20, ErrorMessage = "Must be between 6 and 20 characters", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [Display(Name = "*Confirm your password")]
        public string ConfirmPassword { get; set; }

    }
}
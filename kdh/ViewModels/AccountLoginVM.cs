using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace kdh.ViewModels
{
    public class AccountLoginVM
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [StringLength(100, ErrorMessage = "Field must be less than 100 characters.")]
        [Display(Name = "* Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [Display(Name = "* Password")]
        public string Password { get; set; }

        public string Role { get; set; }
    }
}
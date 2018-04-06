using kdh.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace kdh.ViewModels
{
    public class PatientRegistrationVM
    {

        [Required(AllowEmptyStrings = false, ErrorMessage = "First name is required. ")]
        [StringLength(50, ErrorMessage = "Field must be less than 50 characters. ")]
        [RegularExpression("^[A-Za-z '-.]*$", ErrorMessage = "First name can consist of alphabet, space( ), hyphen(-), apostrophe(') and period(.). ")]
        [Display(Name = "* First Name")]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Last name is required. ")]
        [StringLength(50, ErrorMessage = "Field must be less than 50 characters. ")]
        [RegularExpression("^[A-Za-z '-.]*$", ErrorMessage = "Last name can consist of alphabet, space( ), hyphen(-), apostrophe(') and period(.). ")]
        [Display(Name = "* Last Name")]
        public string LastName { get; set; }

        [StringLength(12, MinimumLength = 10, ErrorMessage = "Field must be 10 or 12 characters.")]
        [RegularExpression(@"\d{10}([A-Z]{2})?", ErrorMessage = "Valid health card number must be 10 digit numbers and 2 alphabets of version code (optional).")]
        [Display(Name = "OHIP Number")]
        public string HealthCardNumber { get; set; }

        [StringLength(100, ErrorMessage = "Field must be less than 100 characters.")]
        [Display(Name = "Address1")]
        public string Address1 { get; set; }

        [StringLength(100, ErrorMessage = "Field must be less than 100 characters.")]
        [Display(Name = "Address2")]
        public string Address2 { get; set; }

        [StringLength(20, ErrorMessage = "Field must be less than 20 characters.")]
        public string City { get; set; }
        public string Province { get; set; }

        [Display(Name = "Postal Code")]
        [RegularExpression("[A-Z][0-9][A-Z] ?[0-9][A-Z][0-9]", ErrorMessage = "Invalid Canadian postal code.")]
        public string PostalCode { get; set; }

        [DataType(DataType.Date)]
        [DateValidator(ErrorMessage = "Date of birth must be in the past." )]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date of Birth")]
        public Nullable<DateTime> DateOfBirth { get; set; }

        [Required(ErrorMessage = "Gender is required. ")]
        [RegularExpression("^Female|Male|Others$", ErrorMessage = "Gender must be choosen from one of the options.")]
        [Display(Name = "* Gender")]
        public string Gender { get; set; }

        [RegularExpression(@"^\d{10,15}$", ErrorMessage = "Invalid phone number. Phone number must be between 10 to 15 digits.")]
        public string Phone { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [StringLength(100, ErrorMessage = "Field must be less than 100 characters.")]
        [Remote("IsAvailableEmail", "Admin", HttpMethod ="POST", ErrorMessage = "Email address is already in use.")]
        public string Email { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace kdh.Models
{
    [Table("Applicant")]
    [MetadataType(typeof(buddy_JobApplicant))]
    public partial class Applicant
    {

    }
    public class buddy_JobApplicant
    {
        [Key]
        public int ApplicantId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "First name is required.")]
        [StringLength(50, ErrorMessage = "First name must be between 2 and 50 characters.", MinimumLength = 2)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Last name is required.")]
        [StringLength(50, ErrorMessage = "Last name must be between 2 and 50 characters.", MinimumLength = 2)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Date of birth should not be later than today.")]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public System.DateTime DateOfBirth { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Email is required.")]
        [StringLength(50, ErrorMessage = "Email must be between 7 and 50 characters.", MinimumLength = 7)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Phone is required.")]
        [StringLength(15, ErrorMessage = "Invalid phone number length", MinimumLength = 9)]
        [Display(Name = "Phone")]
        public string Phone { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Position is required.")]
        [Display(Name = "Job Position")]
        public string JobId { get; set; }

        public virtual Job Job { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace kdh.Models
{
    [Table("OnlineConsultationForm")]
    [MetadataType(typeof(buddy_OCF))]

    public partial class OnlineConsultationForm
    {

    }

    public class buddy_OCF
    {
        [Key]
        [Required(ErrorMessage = "Enter form ID")]
        public Guid Id { get; set; }


        [Display(Name = "First Name")]
        [Required(ErrorMessage = "Enter first name")]
        [RegularExpression("^[A-Za-z]*$", ErrorMessage = "First Name should contain letters only.")]
        [StringLength(20, ErrorMessage = "Maximum 20 characters")]
        public string FirstName { get; set; }


        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Enter last name")]
        [RegularExpression("^[A-Za-z]*$", ErrorMessage = "Last Name should contain letters only.")]
        [StringLength(20, ErrorMessage = "Maximum 20 characters")]
        public string LastName { get; set; }


        [Display(Name = "Date of Birth")]
        [Required(ErrorMessage = "Enter date of birth")]
        public DateTime DateOfBirth { get; set; }


        [Display(Name = "Gender")]
        [Required(ErrorMessage = "Choose gender")]
        public string Gender { get; set; }

        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Enter phone number")]
        [StringLength(15, MinimumLength = 10, ErrorMessage = "Field must be 10 or 15 characters.")]
        [RegularExpression(@"^\d{10,15}$", ErrorMessage = "10 to 15 numeric characters")]
        [Phone(ErrorMessage = "Should be numbers only")]
        public string PhoneNumber { get; set; }


        [Display(Name = "Email")]
        [Required(ErrorMessage = "Enter email")]
        [EmailAddress(ErrorMessage = "Enter email in this format 'email@mail.com'")]
        public string Email { get; set; }


        [Display(Name = "Specialization")]
        [Required(ErrorMessage = "Choose specialization")]
        public string Specialization { get; set; }


        [Display(Name = "Comment")]
        [StringLength(500, ErrorMessage = "Maximum 500 characters")]
        public string Comment { get; set; }


        [Display(Name = "Date of Consultation")]
        [Required(ErrorMessage = "Enter date of consultation")]
        [DataType(DataType.DateTime)]
        public DateTime DateOfConsultation { get; set; }

    }
}
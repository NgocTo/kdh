using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kdh.Models
{
    [Table("Doctors")]
    [MetadataType(typeof(DoctorMeta))]
    public partial class Doctor
    {
    }
    public class DoctorMeta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name ="Doctor id :")]
        public int Doctorid { get; set; }

        [StringLength(100, MinimumLength = 3, ErrorMessage = "Field must be 3 or 100 characters.")]
        [RegularExpression("^[A-Za-z]*$", ErrorMessage = " First Name can consist of alphabets only.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "First Name Required")]
        [Display(Name = "First Name :")]
        public string Fname { get; set; }

        [StringLength(100, MinimumLength = 3, ErrorMessage = "Field must be 3 or 100 characters.")]
        [RegularExpression("^[A-Za-z]*$", ErrorMessage = "Last Name can consist of alphabets only.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Last Name Required")]
        [Display(Name = "Last Name :")]
        public string Lname { get; set; }

        [StringLength(100, MinimumLength = 6, ErrorMessage = "Field must be 6 or 100 characters.")]
        [EmailAddress(ErrorMessage ="Invalid Email")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email Required")]
        [Display(Name = "Email :")]
        public string Email { get; set; }

        [StringLength(50, MinimumLength = 6, ErrorMessage = "Field must be 6 or 50 characters.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Address Required")]
        [Display(Name = "Street Address :")]
        public string Address_line1 { get; set; }

        [StringLength(50, MinimumLength = 0, ErrorMessage = "Field must be max 50 characters.")]
        [Display(Name = "Unit no :")]
        public string Address_line2 { get; set; }


        [RegularExpression("[A-Z][0-9][A-Z] ?[0-9][A-Z][0-9]", ErrorMessage = "Invalid postal code. 6 characters alphanumeric ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Postal Code Required")]
        [Display(Name = "Postal Code")]
        public string Postal_code { get; set; }

        [StringLength(15, MinimumLength = 6, ErrorMessage = "Field must be 10 or 15 characters.")]
        [RegularExpression(@"^\d{10,15}$", ErrorMessage = "10 to 15 numeric characters")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Mobile No required")]
        [Display(Name = "Mobile No")]
        public string Mobile_no { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "Date of join Required")]
        [Display(Name = "joining Date")]
        public DateTime Date_of_join { get; set; }

        [StringLength(40, MinimumLength = 6, ErrorMessage = "Field must be 6 or 40 characters.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Province Required")]
        [RegularExpression("^[ A-Za-z]*$", ErrorMessage = " Province can consist of alphabets only.")]
        [Display(Name = "Province")]
        public string Province { get; set; }

        [StringLength(40, MinimumLength = 6, ErrorMessage = "Field must be 6 or 40 characters.")]
        [RegularExpression("^[ A-Za-z]*$", ErrorMessage = " city can consist of alphabets only.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "City required")]
        [Display(Name = "City")]
        public string City { get; set; }

        [StringLength(20, MinimumLength = 6, ErrorMessage = "Field must be 6 or 20 characters.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Speciality required")]
        [RegularExpression("^[ A-Za-z]*$", ErrorMessage = " speciality can consist of alphabets only.")]
        [Display(Name = "Speciality")]
        public string Speciality { get; set; }

        //navigation property defining many to one
        [Display(Name = "Department")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Department Required")]
        public int Departmentid { get; set; }

        public virtual department department { get; set; }
    }
}
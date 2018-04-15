using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace kdh.Models
{
    [Table("DonationContacts")]
    [MetadataType(typeof(buddy_donateContact))]
    public partial class DonationContact
    {

    }
    public class buddy_donateContact
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int DonorId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "First Name is required.")]
        [RegularExpression("^[A-Za-z]*$", ErrorMessage = " First name can consist of alphabets only.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First Name must be 2 to 50 characters long.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Last Name is required.")]
        [RegularExpression("^[A-Za-z]*$", ErrorMessage = " Last name can consist of alphabets only.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Last Name must be 2 to 50 characters long.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "E-mail is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Phone is required.")]
        [RegularExpression("^[0-9]{3}[-][0-9]{3}[-][0-9]{4}$", ErrorMessage = "Invalid Phone Number.Example: 111-111-1111")]
        [Display(Name = "Phone")]
        public string Phone { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Address is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Address must be 2 to 100 characters long.")]
        [Display(Name = "Address")]
        public string DonorAddress { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "City Name is required.")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "City must be 2 to 20 characters long.")]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Postal Code is required.")]
        [RegularExpression("^[a-zA-Z][0-9][a-zA-Z][0-9][a-zA-Z][0-9]$", ErrorMessage = "Invalid Postal code.Example: A1A1A1")]
        [StringLength(6, ErrorMessage = "Postal Code must be 6 characters long.")]
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Country is required.")]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Country can consist of alphabets only.")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Country must be 2 to 20 characters long.")]
        [Display(Name = "Country")]
        public string Country { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Province is required.")]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Province can consist of alphabets only.")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Province must be 2 to 30 characters long.")]
        [Display(Name = "Province")]
        public string Province { get; set; }

        [StringLength(200, MinimumLength = 2, ErrorMessage = "Comments violates size limits.It allows only 2 to 200 characters.")]
        [Display(Name = "Comments")]
        public string Comments { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Date is required.")]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{yyyy/mm/dd}")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Donation Date")]
        public System.DateTime DonationDate { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Amount is required.")]
        [Display(Name = "Amount")]
        [DataType(DataType.Currency)]
        [RegularExpression(@"^[0-9]+(\.[0-9]{1,2})$", ErrorMessage = "Valid Decimal number with maximum 2 decimal places.")]
        public decimal Amount { get; set; }

        [Display(Name = "Payment ID")]
        public string PaymentId { get; set; }

        

    }
}
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
        [StringLength(100, MinimumLength = 2, ErrorMessage = "First Name must be 2 to 100 characters long.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Last Name is required.")]
        [RegularExpression("^[A-Za-z]*$", ErrorMessage = " Last name can consist of alphabets only.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Last Name must be 2 to 100 characters long.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [RegularExpression("^[A-Za-z]*$", ErrorMessage = " Last name can consist of alphabets only.")]
        [Display(Name = "Last Name")]
        public string Email { get; set; }

        [RegularExpression("^[A-Za-z]*$", ErrorMessage = " Last name can consist of alphabets only.")]
        [Display(Name = "Phone")]
        public string Phone { get; set; }
        public string DonorAddress { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Province { get; set; }
        public string Comments { get; set; }
        public System.DateTime DonationDate { get; set; }
        public decimal Amount { get; set; }
        public Nullable<int> CreditCardId { get; set; }

        public virtual DonateCreditCard DonateCreditCard { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace kdh.Models
{
    [MetadataType(typeof(FAQMeta))]
    public partial class FAQ
    {
    }
    public class FAQMeta
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int QueId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Question is required.")]
        [Display(Name = "Question")]
        public string Question { get; set; }

        [Display(Name = "Answer")]
        public string Answer { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Date Created is required.")]
        [Display(Name = "Date Created")]
        public System.DateTime DateCreated { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Author First Name is required.")]
        [RegularExpression("^[A-Za-z]*$", ErrorMessage = " Author name can consist of alphabets only.")]
        [Display(Name = "Author First Name")]
        [DataType(DataType.Text)]
        public string AuthorFirstName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Authority First Name is required.")]
        [RegularExpression("^[A-Za-z]*$", ErrorMessage = " Authority name can consist of alphabets only.")]
        [Display(Name = "Authority First Name")]
        [DataType(DataType.Text)]
        public string AuthorityFirstName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Purpose is required")]
        [Display(Name = "Purpose")]
        public string PurposeId { get; set; }

        public virtual Purpos Purpos { get; set; }
    }
}
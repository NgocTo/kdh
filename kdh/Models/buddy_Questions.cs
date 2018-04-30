using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kdh.Models
{
    [Table("Questions")]
    [MetadataType(typeof(buddy_Questions))]
    public partial class Question
    {

    }
    public class buddy_Questions
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

       

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address ")]
        public string Email { get; set; }

        [RegularExpression("^[A-Za-z ]*$", ErrorMessage = "Your name can only contain characters.")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "3 to 20 characters")]
        [Display(Name = "Name ")]
        public string Name { get; set; }

        [StringLength(160, MinimumLength = 3, ErrorMessage = "3 to 160 characters")]
        [Display(Name = "Subject ")]
        public string subject { get; set; }

        [StringLength(250, MinimumLength = 3, ErrorMessage = "3 to 250 characters")]
        [Display(Name = "Ask Question")]
        public string question1 { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [DataType(DataType.DateTime)]
        [Display(Name = "Date posted")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime create_date { get; set; }

       


    }
}
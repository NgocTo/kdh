using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kdh.Models
{
    [Table("Answers")]
    [MetadataType(typeof(buddy_Answers))]
    public partial class Answer
    {

    }
    public class buddy_Answers
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }



        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address ")]
        public string Sender_mail { get; set; }

        


        [StringLength(250, MinimumLength = 3, ErrorMessage = "3 to 250 characters")]
        [Display(Name = "Answer")]
        public string Answer1 { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [DataType(DataType.DateTime)]
        [Display(Name = "Date posted")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Create_date { get; set; }


        public Question Question { get; set; }

        public int Questionsid { get; set; }

    }
}
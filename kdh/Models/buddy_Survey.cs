using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace kdh.Models
{
    [Table("Survey")]
    [MetadataType(typeof(buddy_Survey))]
    public partial class Survey
    {

    }
    public class buddy_Survey
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide your username.")]
        [Display(Name = "Name that appears on survey")]
        public string UserName { get; set; }

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [DataType(DataType.Date)]
        //CompareValidator1.ValueToCompare = DateTime.Today.ToString("MM/dd/yyyy");
        [Display(Name = "Date")]
        public DateTime DateOfSurvey { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please choose one option.")]
        [Display(Name = "Overall how would you rate the quality of service in" +
                        "Humber District Hospital?")]
        public string QualityService { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please choose one option.")]
        [Display(Name = "On average how often do you visit the hospital in a given year?")]
        public string AverageVisit { get; set; }
        
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please choose one option.")]
        [Display(Name = "Did you have any issues arranging an appointment with the doctor?")]
        public string AppointmentIssue { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please choose one option.")]
        [Display(Name = "How would you rate the professionalism of our staff?")]
        public string StaffRate { get; set; }

        [StringLength(500, ErrorMessage = "Comment can't be more than 500 characters.")]
        [Display(Name = "Any other wish or concern?")]
        public string Comment { get; set; }
    }
}
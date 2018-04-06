using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace kdh.Models
{
    [Table("Job")]
    [MetadataType(typeof(buddy_Job))]
    public partial class Job
    {

    }
    public class buddy_Job
    {
        [Key]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Id field is required.")]
        [StringLength(50, ErrorMessage = "Input must be between 2 and 50 characters.", MinimumLength = 2)]
        [Display(Name = "Id")]
        public string JobId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Job title field is required.")]
        [StringLength(50, ErrorMessage = "Input must be between 5 and 50 characters.", MinimumLength = 5)]
        [Display(Name = "Job Title")]
        public string JobTitle { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Job status field is required.")]
        [StringLength(50, ErrorMessage = "Input must be between 5 and 50 characters.", MinimumLength = 5)]
        [Display(Name = "Status")]
        public string JobStatus { get; set; }

        [Display(Name = "Description")]
        public string JobDescription { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please select a department.")]
        [StringLength(500, ErrorMessage = "Input must be less than 500 characters.")]
        [Display(Name = "Department")]
        public string DepartmentId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Date posted for job must be later than today.")]
        [DataType(DataType.Date)]
        [Display(Name = "Date posted")]
        public Nullable<DateTime> DatePosted { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Date closed for job must be later than today.")]
        [Display(Name = "Date closed")]
        [DataType(DataType.DateTime)]
        public Nullable<DateTime> DateClosed { get; set; }

        [Display(Name = "Shift")]
        [StringLength(50, ErrorMessage = "Input must be between 5 and 50 characters.", MinimumLength = 5)]
        public string JobShift { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Salary for job is required.")]
        [StringLength(50, ErrorMessage = "Input must be between 5 and 50 characters.", MinimumLength = 5)]
        [Display(Name = "Salary")]
        public string Salary { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Job requirement field is required.")]
        [StringLength(500, ErrorMessage = "Input must be less than 500 characters.")]
        [Display(Name = "Requirement")]
        public string Requirement { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Job manager's email field is required.")]
        [Display(Name = "Manager's Email")]
        public int UserId { get; set; }
    }
}
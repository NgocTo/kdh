using kdh.Models;
using kdh.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace kdh.ViewModels
{
    public class LabReportVM
    {
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }

        [DataType(DataType.Date)]
        [DateValidator(ErrorMessage = "Collection date must be in the past.")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Test Collected on")]
        public DateTime CollectionDate { get; set; }

        [DataType(DataType.Date)]
        [DateValidator(ErrorMessage = "Issue date must be in the past.")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Report Issued on")]
        public Nullable<DateTime> IssueDate { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [StringLength(20, ErrorMessage = "Must be less than 20 characters.")]
        [Display(Name = "*Status")]
        public string Status { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [StringLength(50, ErrorMessage = "Must be less than 50 characters.")]
        [Display(Name = "*Test Ordered By")]
        public string OrderedBy { get; set; }

    }
}
using kdh.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace kdh.ViewModels
{
    public class ReportResultVM
    {

        public LabReport LabReport { get; set; }

        public TestType TestType { get; set; }
        //public List<Result> Results { get; set; }

        public Guid Id { get; set; }
        public Guid ReportId { get; set; }
        public Guid TestId { get; set; }
        public string Flag { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [Range(-999999, 999999, ErrorMessage = "Please enter valid float Number")]
        [Display(Name = "*Result")]
        public double Result1 { get; set; }

        [StringLength(300, ErrorMessage = "Must be less than 300 characters.")]
        [Display(Name = "Note")]
        public string Note { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace kdh.Models
{
    [MetadataType(typeof(buddy_Testimonial))]
    public partial class Testimonial
    {
    }
    public class buddy_Testimonial
    {
        [Key]
        [Display(Name = "Testimonial #")]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your name.")]
        [Display(Name = "Your name")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Your name should be at least 2 characters long")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please select who you are.")]
        [Display(Name = "Who you are")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Your role should be at least 2 characters long")]
        public string Role { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Subject is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Subject should be at least 2 characters long")]
        public string Subject { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Content is required.")]
        [StringLength(500, MinimumLength = 4, ErrorMessage = "Content should be at least 4 characters long")]
        public string Content { get; set; }

        [Required(ErrorMessage = "Please rate us.")]
        [Display(Name = "Rate us")]
        [Range(1, 5, ErrorMessage = "Rating can only be between 1 and 5")]
        public Nullable<int> Rate { get; set; }

        public string Reviewed { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please select a department.")]
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }
        
    }
}
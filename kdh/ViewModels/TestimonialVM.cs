using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace kdh.ViewModels
{
    public class TestimonialVM
    {
        [Key]
        [Display(Name = "Testimonial #")]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Subject is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Subject should be at least 2 characters long")]
        public string Subject { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Content is required.")]
        [StringLength(500, MinimumLength = 4, ErrorMessage = "Content should be at least 4 characters long")]
        public string Content { get; set; }

        public string Reviewed { get; set; }
    }
}
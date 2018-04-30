using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace kdh.ViewModels
{
    public class TestTypeVM
    {

        public System.Guid Id { get; set; }

        [Required(ErrorMessage = "Category is required")]
        [StringLength(20, ErrorMessage = "Must be less than 20 characters.")]
        [Display(Name = "*Category")]
        public string Category { get; set; }

        [Required(ErrorMessage = "Test Item is required")]
        [StringLength(20, ErrorMessage = "Must be less than 20 characters.")]
        [Display(Name = "*Test Item")]
        public string TestItem { get; set; }

        [Required(ErrorMessage = "Maximum Reference is required")]
        [Display(Name = "*Maximum Reference")]
        [Range(-999999, 999999, ErrorMessage = "Please enter valid float Number")]
        public double MaxReference { get; set; }

        [Required(ErrorMessage = "Minimum Reference is required")]
        [Display(Name = "*Minimum Reference")]
        [Range(-999999, 999999, ErrorMessage = "Please enter valid float Number")]
        public double MinReference { get; set; }

        [Required(ErrorMessage = "Unit is required")]
        [StringLength(10, ErrorMessage = "Must be less than 10 characters.")]
        [Display(Name = "*Unit")]
        public string Unit { get; set; }

    }
}
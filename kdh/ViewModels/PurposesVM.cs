using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace kdh.ViewModels
{
    [Table("Purposes")]
    public class PurposesVM
    {

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Purpose Id")]
        public int PurposeId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Purpose is required")]
        [Display(Name = "Purpose To Create")]
        [StringLength(900, MinimumLength = 2, ErrorMessage = "String should be at least 2 characters long")]
        public string PurposeToCreate { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kdh.Models
{
    [Table("departments")]
    [MetadataType(typeof(departmentMeta))]
    public partial class department
    {
    }
    public class departmentMeta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "department id")]
        public int departmentid { get; set; }
        [Required(ErrorMessage = "enter department name")]
        [StringLength(50, ErrorMessage = "maximum 50 characters")]
        [Display(Name = "department Name")]
        public string department_name { get; set; }
        [Required(ErrorMessage = "enter department location")]
        [StringLength(10, ErrorMessage = "maximum 10 characters")]
        [Display(Name = "department Location")]
        public string department_location { get; set; }
        [Required(ErrorMessage = "give some description")]
        [StringLength(100, ErrorMessage = "not more than 100 characters")]
        [Display(Name = "Description")]
        public string department_description { get; set; }

        //navigation property defining one to many
        public ICollection<Doctor> Doctors { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace kdh.Models
{
    [Table("Users")]
    [MetadataType(typeof(buddy_User))]
   
    public partial class User
    {
        
    }
    public class buddy_User
    {
        [Key]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Id is required.")]
        [Display(Name = "User ID")]
        public System.Guid Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Email is required.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required.")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Role is required.")]
        [Display(Name = "Role")]
        public string Role { get; set; }
    }

}
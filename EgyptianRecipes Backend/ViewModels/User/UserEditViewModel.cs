using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class UserEditViewModel
    {
        [Required]
        public int ID { get; set; }
        [Required]
        [MaxLength(25, ErrorMessage = "Name must be less than 20 character")]
        public string Name { get; set; }
        [Required]
        [MaxLength(20, ErrorMessage = "UserName must be less than 20 character")]
        public string UserName { get; set; }
        [Required]
        [MaxLength(20 ,ErrorMessage ="Password must be less than 20 character")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        [Required]
        [EmailAddress]
        //[Remote("EmailValidation", "Account", ErrorMessage = "{0} already has an account, please enter a different email address.")]
        public string Email { get; set; }
        [Required]
        [MaxLength(11, ErrorMessage = "Phone must be less than 11 character")]
        public string Phone { get; set; }
        [Required]
        public string Photo { get; set; }
        public string Role { get; set; }

    }
}

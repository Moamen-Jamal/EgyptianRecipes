using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class UserRoleEditViewModel
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public string RoleName { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string UserPassword { get; set; }
        [Required]
        public int RoleID { get; set; }
        [Required]
        public int UserID { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class UserLoginViewModel
    {
        [Required(ErrorMessage ="إسم المرور مطلوب")]
        [MinLength(3)]
        [MaxLength(20)]
        public string UserName { get; set; }
        [Required(ErrorMessage ="كلمة المرور مطلوبة")]
        [MinLength(3)]
        [MaxLength(20)]
        public string Password { get; set; }
    }
}

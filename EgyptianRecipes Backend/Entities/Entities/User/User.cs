using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class User : BaseModel
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Photo { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Admin Admin { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}

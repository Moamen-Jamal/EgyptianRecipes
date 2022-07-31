using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class Role : BaseModel
    {
        public string Name { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}

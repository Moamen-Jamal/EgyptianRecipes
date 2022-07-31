using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class Customer : BaseModel
    {

        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<CustomerBranch> CustomerBranches { get; set; }

    }
}

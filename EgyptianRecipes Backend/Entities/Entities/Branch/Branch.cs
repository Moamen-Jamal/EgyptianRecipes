using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class Branch : BaseModel
    {
        public string Title { get; set; }
        public string ManagerName { get; set; }
        public DateTime OpeningHour { get; set; }
        public DateTime ClosingHour { get; set; }
        public virtual ICollection<CustomerBranch> CustomerBranches { get; set; }

    }
}

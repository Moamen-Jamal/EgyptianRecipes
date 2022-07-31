using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class CustomerBranch : BaseModel
    {
        public virtual Customer Customer { get; set; }
        public int CustomerID { get; set; }
        public virtual Branch Branch { get; set; }
        public int BranchID { get; set; }
    }
}

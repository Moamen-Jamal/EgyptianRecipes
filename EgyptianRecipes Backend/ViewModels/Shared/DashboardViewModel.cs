using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class DashboardViewModel
    {
        public int TotalBranches { get; set; }
        public int BranchesToday { get; set; }
        public int TotalCustomers { get; set; }
        public int CustomersToday { get; set; }
        public int TotalAdmins{ get; set; }
        public int AdminsToday { get; set; }
    }
}

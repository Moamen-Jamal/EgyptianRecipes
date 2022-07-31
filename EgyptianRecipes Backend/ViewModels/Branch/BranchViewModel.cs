using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class BranchViewModel
    {
        public int ID { get; set; }
        public DateTime OpeningHour { get; set; }
        public DateTime ClosingHour { get; set; }
        public string ManagerName { get; set; }
        public string Title { get; set; }

    }
}

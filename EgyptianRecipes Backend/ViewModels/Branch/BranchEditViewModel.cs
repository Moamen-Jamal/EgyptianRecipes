using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class BranchEditViewModel
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public DateTime ClosingHour { get; set; }
        [Required]
        public DateTime OpeningHour { get; set; }
        [Required]
        public string ManagerName { get; set; }
        [Required]
        public string Title { get; set; }
        public int CustomerID { get; set; }

    }
}

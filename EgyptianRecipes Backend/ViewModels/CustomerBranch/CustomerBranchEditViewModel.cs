using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class CustomerBranchEditViewModel
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public string CustomerName { get; set; }
        [Required]
        public string BranchTitle { get; set; }
        [Required]
        public int CustomerID { get; set; }
        [Required]
        public int BranchID { get; set; }
    }
}

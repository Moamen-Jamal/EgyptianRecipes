using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public static class CustomerBranchExtensions
    {
        public static CustomerBranchEditViewModel ToEditableViewModel(this CustomerBranch model)
        {
            return new CustomerBranchEditViewModel
            {
                ID = model.ID,
                BranchTitle = model.Branch.Title,
                CustomerName = model.Customer.User.Name,
                CustomerID = model.CustomerID,
                BranchID = model.BranchID
            };
        }
        public static CustomerBranch ToModel(this CustomerBranchEditViewModel model)
        {
            return new CustomerBranch
            {
                ID = model.ID,
                BranchID = model.BranchID,
                CustomerID = model.CustomerID
            };
        }
        public static CustomerBranchViewModel ToViewModel(this CustomerBranch model)
        {
            return new CustomerBranchViewModel
            {
                ID = model.ID,
                BranchTitle = model.Branch.Title,
                CustomerName = model.Customer.User.Name
            };
        }
    }
}

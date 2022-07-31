using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public static class BranchExtensions
    {
        public static BranchEditViewModel ToEditableViewModel(this Branch model)
        {
            return new BranchEditViewModel
            {
                ID = model.ID,
                ClosingHour = model.ClosingHour,
                ManagerName = model.ManagerName,
                OpeningHour = model.OpeningHour,
                Title = model.Title
            };
        }
        public static Branch ToModel(this BranchEditViewModel model)
        {
            return new Branch
            {
                ID = model.ID,
                Title = model.Title,
                OpeningHour = model.OpeningHour,
                ManagerName = model.ManagerName,
                ClosingHour = model.ClosingHour
            };
        }
        public static BranchViewModel ToViewModel(this Branch model)
        {
            return new BranchViewModel
            {
                ID = model.ID,
                Title = model.Title,
                ManagerName = model.ManagerName,
                OpeningHour = model.OpeningHour,
                ClosingHour = model.ClosingHour
                
            };
            
        }
    }
}

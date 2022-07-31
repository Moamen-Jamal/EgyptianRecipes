using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public static class RoleExtensions
    {
        public static RoleEditViewModel ToEditableViewModel(this Role model)
        {
            return new RoleEditViewModel
            {
                ID = model.ID,
                Name = model.Name
                
            };
        }
        public static Role ToModel(this RoleEditViewModel model)
        {
            return new Role
            {
                ID = model.ID,
                Name = model.Name
                
            };
        }
        public static RoleViewModel ToViewModel(this Role model)
        {
            return new RoleViewModel
            {
                ID = model.ID,
                Name = model.Name
            };
        }
    }
}

using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public static class UserRoleExtensions
    {
        public static UserRoleEditViewModel ToEditableViewModel(this UserRole model)
        {
            return new UserRoleEditViewModel
            {
                ID = model.ID,
                RoleName = model.Role.Name,
                UserName = model.User.UserName,
                UserPassword =model.User.Password,
                RoleID = model.RoleID,
                UserID = model.UserID
            };
        }
        public static UserRole ToModel(this UserRoleEditViewModel model)
        {
            return new UserRole
            {
                ID = model.ID,
                User =new User
                {
                    
                    UserName = model.UserName,
                    Password = model.UserPassword,
                },
                
                Role = new Role
                {
                    Name = model.RoleName,
                },
            };
        }
        public static UserRoleViewModel ToViewModel(this UserRole model)
        {
            return new UserRoleViewModel
            {
                ID = model.ID,
                RoleName = model.Role.Name,
                UserName = model.User.UserName,
                UserPassword = model.User.Password,
            };
        }
    }
}

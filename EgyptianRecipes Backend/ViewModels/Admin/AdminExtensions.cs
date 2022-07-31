using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public static class AdminExtensions
    {
        public static AdminEditViewModel ToEditableViewModel (this Admin model)
        {
            return new AdminEditViewModel
            {
                ID = model.ID,
                Name = model.User.Name,
                UserName = model.User.UserName,
                Password = model.User.Password,
                Email = model.User.Email,
                Phone = model.User.Phone,
                Photo = model.User.Photo,
                //ConfirmPassword = model.User.Password,
                
            };
        }
        public static Admin ToModel(this AdminEditViewModel model)
        {
            return new Admin
            { ID = model.ID ,
                User = new User
                {
                    ID = model.ID ,
                    Name = model.Name,
                    UserName = model.UserName,
                    Password = model.Password,
                    Email = model.Email,
                    Phone = model.Phone,
                    Photo = model.Photo,
                    UserRoles = new List<UserRole>()
                    {
                        new UserRole()
                        {
                           ID = 0 ,
                           RoleID = 1 ,
                           UserID = model.ID
                        }
                    }
                },
            };
        }
        public static AdminViewModel ToViewModel(this Admin model)
        {
            return new AdminViewModel
            {
                ID = model.ID,
                Name = model.User.Name,
                UserName = model.User.UserName,
                Password = model.User.Password,
                Email = model.User.Email,
                Phone = model.User.Phone,
                Photo = model.User.Photo,

            };
        }
    }
}

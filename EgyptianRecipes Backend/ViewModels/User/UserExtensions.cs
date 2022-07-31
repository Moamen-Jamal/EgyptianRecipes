using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public static class UserExtensions
    {
        public static UserEditViewModel ToEditableViewModel (this User model)
        {
            return new UserEditViewModel
            {
                ID = model.ID,
                Name = model.Name,
                UserName = model.UserName,
                Password = model.Password,
                ConfirmPassword = model.Password,
                Email = model.Email,
                Phone = model.Phone,
                Photo = model.Photo,
                Role = model.UserRoles.FirstOrDefault().Role.Name ,
            };
        }
        public static User ToModel(this UserEditViewModel model)
        {
            return new User
            {
                ID = model.ID,
                Name = model.Name,
                UserName = model.UserName,
                Password = model.Password,
                Email = model.Email,
                Phone = model.Phone,
                Photo = model.Photo,
            };
        }
        public static UserViewModel ToViewModel(this User model)
        {
            return new UserViewModel
            {
                ID = model.ID,
                Name = model.Name,
                UserName = model.UserName,
                Password = model.Password,
                Email = model.Email,
                Phone = model.Phone,
                Photo = model.Photo ,
                Role = model.UserRoles.FirstOrDefault().Role.Name,
            };
        }
    }
}

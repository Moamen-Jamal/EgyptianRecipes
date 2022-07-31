using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public static class CustomerExtensions
    {
        public static CustomerEditViewModel ToEditableViewModel (this Customer model)
        {
            return new CustomerEditViewModel()
            {
                ID = model.ID,
                Name = model.User.Name,
                UserName = model.User.UserName,
                Password = model.User.Password,
                Email = model.User.Email,
                Phone = model.User.Phone,
                Photo = model.User.Photo,
                Gender = model.Gender,
                BirthDate = model.BirthDate,
                
            };
        }
        public static Customer ToModel(this CustomerEditViewModel model)
        {
            return new Customer()
            {
                ID = model.ID,
                User = new User
                {
                    ID = model.ID,
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
                           RoleID = 2 ,
                           UserID = model.ID
                        }
                    }
                },
                Gender = model.Gender,
                BirthDate = model.BirthDate,
            };
        }
        public static CustomerViewModel ToViewModel(this Customer model)
        {
            return new CustomerViewModel()
            {
                ID = model.ID,
                Name = model.User.Name,
                UserName = model.User.UserName,
                Password = model.User.Password,
                Email = model.User.Email,
                Phone = model.User.Phone,
                Photo = model.User.Photo,
                Gender = model.Gender,
                BirthDate = model.BirthDate,
            };
        }
    }
}

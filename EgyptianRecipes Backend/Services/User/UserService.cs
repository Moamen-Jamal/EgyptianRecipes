using Entities.Entities;
using Helpers;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace Services
{
    public class UserService
    {
        UnitOfWork unitOfWork;
        Generic<User> UserRepo;
        SecurityHelper SecurityHelper;
        public UserService(UnitOfWork _unitOfWork,
            SecurityHelper _SecurityHelper)
        {
            unitOfWork = _unitOfWork;
            UserRepo = unitOfWork.UserRepo;
            SecurityHelper = _SecurityHelper;
        }

        public UserEditViewModel Add (UserEditViewModel User)
        {
            User _User = UserRepo.Add(User.ToModel());
            unitOfWork.commit();
            return _User.ToEditableViewModel() ;
        }
        public UserEditViewModel Update(UserEditViewModel User)
        {
            User _User = UserRepo.Update(User.ToModel());
            unitOfWork.commit();
            return _User.ToEditableViewModel();
        }
        public void Remove(int id)
        {
            UserRepo.Remove(new User { ID = id});
            unitOfWork.commit();
        }

        public UserViewModel GetByID(int id)
        {
            return UserRepo.GetByID(id).ToViewModel();
        }
        public IEnumerable<UserViewModel> GetAll(string UserName = "" , string Email = "")
        {
            var query =
                UserRepo.GetAll();

            if (!string.IsNullOrEmpty(UserName))
                query = query.Where(i => i.UserName == UserName);

            if (!string.IsNullOrEmpty(UserName))
                query = query.Where(i => i.Email == Email);

            return query.ToList().Select(i => i.ToViewModel());
        }
        
        public IEnumerable<UserViewModel> Get(int id=0 , string name="",string phone = "" , int pageIndex=0 , int PageSize=20)
        {
            var query =
                UserRepo.GetAll();
            if (id > 0 )
                query = query.Where(i => i.ID == id );
            
            if (!string.IsNullOrEmpty(name))
                query = query.Where(i=> i.Name == name);
            
            if (!string.IsNullOrEmpty(phone))
                query = query.Where(i => i.Phone == phone);
            
            query = query.Skip(pageIndex * PageSize).Take(PageSize);

            return query.ToList().Select(i => i.ToViewModel());
        }

        public UserViewModel Login(UserLoginViewModel model)
        {
            UserViewModel result = null;
            User selectedUser = 
                UserRepo.Get(i => i.UserName == model.UserName
                && i.Password == model.Password).FirstOrDefault();

            if (selectedUser == null)
                return null;

            result = new UserViewModel();
            result = selectedUser.ToViewModel();
            result.Token =  SecurityHelper.GenerateToken(selectedUser);

            return result;
        }
    }
}

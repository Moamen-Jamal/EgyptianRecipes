using Entities.Entities;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace Services
{
    public class UserRoleService
    {
        UnitOfWork unitOfWork;
        Generic<UserRole> UserRoleRepo;
        public UserRoleService(UnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
            UserRoleRepo = unitOfWork.UserRoleRepo;
        }

        public UserRoleEditViewModel Add (UserRoleEditViewModel UserRole)
        {
            UserRole _UserRole = UserRoleRepo.Add(UserRole.ToModel());
            unitOfWork.commit();
            return _UserRole.ToEditableViewModel() ;
        }
        public UserRoleEditViewModel Update(UserRoleEditViewModel UserRole)
        {
            UserRole _UserRole = UserRoleRepo.Update(UserRole.ToModel());
            unitOfWork.commit();
            return _UserRole.ToEditableViewModel();
        }
        public void Remove(int id)
        {
            UserRoleRepo.Remove(new UserRole { ID = id});
            unitOfWork.commit();
        }

        public UserRoleViewModel GetByID(int id)
        {
            return UserRoleRepo.GetByID(id).ToViewModel();
        }
        public IEnumerable<UserRoleViewModel> GetAll()
        {
            var query =
                UserRoleRepo.GetAll();
            return query.ToList().Select(i => i.ToViewModel());
        }
        public IEnumerable<UserRoleViewModel> Get(int id=0 , int pageIndex=0 , int PageUserRole=20)
        {
            var query =
                    UserRoleRepo.GetAll();
            if (id > 0 )
            {
                 query = query.Where(i => i.ID == id );
            }
            
            
            query = query.Skip(pageIndex * PageUserRole).Take(PageUserRole);

            return query.ToList().Select(i => i.ToViewModel());
        }

    }
}

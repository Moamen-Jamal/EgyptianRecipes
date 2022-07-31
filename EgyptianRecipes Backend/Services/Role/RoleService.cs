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
    public class RoleService
    {
        UnitOfWork unitOfWork;
        Generic<Role> RoleRepo;
        public RoleService(UnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
            RoleRepo = unitOfWork.RoleRepo;
        }

        public RoleEditViewModel Add (RoleEditViewModel Role)
        {
            Role _Role = RoleRepo.Add(Role.ToModel());
            unitOfWork.commit();
            return _Role.ToEditableViewModel() ;
        }
        public RoleEditViewModel Update(RoleEditViewModel Role)
        {
            Role _Role = RoleRepo.Update(Role.ToModel());
            unitOfWork.commit();
            return _Role.ToEditableViewModel();
        }
        public void Remove(int id)
        {
            RoleRepo.Remove(new Role { ID = id});
            unitOfWork.commit();
        }

        public RoleViewModel GetByID(int id)
        {
            return RoleRepo.GetByID(id).ToViewModel();
        }
        public IEnumerable<RoleViewModel> GetAll()
        {
            var query =
                RoleRepo.GetAll();
            return query.ToList().Select(i => i.ToViewModel());
        }
        public IEnumerable<RoleViewModel> Get(int id=0 , string name="",  int pageIndex=0 , int PageRole=20)
        {
            var query =
                    RoleRepo.GetAll();
            if (id > 0 )
            {
                 query = query.Where(i => i.ID == id );
            }
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(i=> i.Name == name);
            }
            
            query = query.Skip(pageIndex * PageRole).Take(PageRole);

            return query.ToList().Select(i => i.ToViewModel());
        }

    }
}

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
    public class AdminService
    {
        UnitOfWork unitOfWork;
        Generic<Admin> AdminRepo;
        Generic<User> UserRepo;
        public AdminService(UnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
            AdminRepo = unitOfWork.AdminRepo;
            UserRepo = unitOfWork.UserRepo;
        }

        public AdminEditViewModel Add (AdminEditViewModel admin)
        {
            Admin _admin = AdminRepo.Add(admin.ToModel());
            unitOfWork.commit();
            return _admin.ToEditableViewModel() ;
        }
        public AdminEditViewModel Update(AdminEditViewModel admin)
        {
            Admin adminModel = admin.ToModel();
            Admin _admin = AdminRepo.Update(adminModel);
            UserRepo.Update(adminModel.User);
            unitOfWork.commit();
            return _admin.ToEditableViewModel();
        }
        public void Remove(int id)
        {
            Admin del =
            AdminRepo.Remove(new Admin { ID = id });
            UserRepo.Remove(new User { ID = id });
            unitOfWork.commit();
            
        }

        public AdminViewModel GetByID(int id)
        {
            return AdminRepo.GetByID(id).ToViewModel();
        }
        public IEnumerable<AdminViewModel> GetAll()
        {
            var query =
                AdminRepo.GetAll();
            return query.ToList().Select(i => i.ToViewModel());
        }
        public PagingViewModel Get(int pageIndex, int pageSize)
        {
            var query =
                AdminRepo.GetAll();


            int records = query.Count();
            if (records <= pageSize || pageIndex <= 0) pageIndex = 1;
            int pages = (int)Math.Ceiling((double)records / pageSize);
            int excludedRows = (pageIndex - 1) * pageSize;

            query = query.OrderBy(i => i.ID);

            query = query.Skip(excludedRows).Take(pageSize);

            var data = query.ToList().Select(i => i.ToViewModel()).ToList();
            return new PagingViewModel()
            { PageIndex = pageIndex, PageSize = pageSize, Result = data, Records = records, Pages = pages };
        }

    }
}

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
    public class CustomerService
    {
        UnitOfWork unitOfWork;
        Generic<Customer> CustomerRepo;
        Generic<User> UserRepo;
        Generic<UserRole> UserRoleRepo;
        Generic<CustomerBranch> CustomerBranchRepo;
        public CustomerService(UnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
            CustomerRepo = unitOfWork.CustomerRepo;
            UserRepo = unitOfWork.UserRepo;
            UserRoleRepo = unitOfWork.UserRoleRepo;
            CustomerBranchRepo = unitOfWork.CustomerBranchRepo;
        }
        public PagingViewModel Get(int pageIndex = 0, int pageSize = 3)
        {
            var query =
                CustomerRepo.GetAll();


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
        public CustomerEditViewModel Add(CustomerEditViewModel Customer)
        {
            Customer _Customer = CustomerRepo.Add(Customer.ToModel());
            unitOfWork.commit();
            return _Customer.ToEditableViewModel();
        }
        public CustomerEditViewModel Update(CustomerEditViewModel Customer)
        {
            Customer customer = Customer.ToModel();
            Customer _Customer = CustomerRepo.Update(customer);
            UserRepo.Update(customer.User);
            unitOfWork.commit();
            return _Customer.ToEditableViewModel();
        }
        public void Remove(int id)
        {
            //var customerBranchQuery = CustomerBranchRepo.GetAll();
            //customerBranchQuery = customerBranchQuery.Where(i => i.CustomerID == id);
            //CustomerBranchRepo.Remove(new CustomerBranch { ID = customerBranchQuery.FirstOrDefault().ID });

            //var userRoleQuery = UserRoleRepo.GetAll();
            //userRoleQuery = userRoleQuery.Where(i => i.UserID == id);
            //UserRoleRepo.Remove(new UserRole { ID = userRoleQuery.FirstOrDefault().ID });

            Customer del =
            CustomerRepo.Remove(new Customer { ID = id });
            UserRepo.Remove(new User { ID = id });
            unitOfWork.commit();
        }

        public CustomerViewModel GetByID(int id)
        {
            return CustomerRepo.GetByID(id).ToViewModel();
        }
        public IEnumerable<CustomerViewModel> GetAll()
        {
            var query =
                CustomerRepo.GetAll();
            return query.ToList().Select(i => i.ToViewModel());
        }
        

    }
}

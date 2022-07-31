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
    public class BranchService
    {
        UnitOfWork unitOfWork;
        Generic<Branch> BranchRepo;
        Generic<CustomerBranch> CustomerBranchRepo;
        Generic<Customer> CustomerRepo;
        public BranchService(UnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
            BranchRepo = unitOfWork.BranchRepo;
            CustomerBranchRepo = unitOfWork.CustomerBranchRepo;
            CustomerRepo = unitOfWork.CustomerRepo;
        }

        public BranchEditViewModel Add(BranchEditViewModel Branch)
        {
            Branch _Branch = BranchRepo.Add(Branch.ToModel());
            unitOfWork.commit();
            return _Branch.ToEditableViewModel();
        }
        public BranchEditViewModel Update(BranchEditViewModel Branch)
        {
            Branch branch = Branch.ToModel();
            Branch _Branch = BranchRepo.Update(branch);
            unitOfWork.commit();
            return _Branch.ToEditableViewModel();
        }
        public BranchEditViewModel Remove(int id)
        {
            Branch del =
            BranchRepo.Remove(new Branch { ID = id });
            unitOfWork.commit();
            return del.ToEditableViewModel();
        }
        public CustomerBranchEditViewModel BookBranch(int branchID, int customerID)
        {
            var query = CustomerBranchRepo.GetAll();
            var selectedBookedBranch = query.Where(i => i.BranchID == branchID && i.CustomerID == customerID);
            if(selectedBookedBranch.Count() > 0)
            {
                return selectedBookedBranch.FirstOrDefault().ToEditableViewModel();
            }
            else
            {
                CustomerBranchEditViewModel customerBranch = new CustomerBranchEditViewModel
                {
                    ID = 0,
                    BranchID = branchID,
                    CustomerID = customerID,
                    BranchTitle = BranchRepo.GetByID(branchID).Title,
                    CustomerName = CustomerRepo.GetByID(customerID).User.Name
                };
                CustomerBranch _CustomerBranch = CustomerBranchRepo.Add(customerBranch.ToModel());
                unitOfWork.commit();
                return _CustomerBranch.ToEditableViewModel();
            }
        }
        public CustomerBranchEditViewModel CancelBookedBranch(int branchID, int customerID)
        {
            var query = CustomerBranchRepo.GetAll();
            var selectedBookedBranch = query.Where(i => i.BranchID == branchID && i.CustomerID == customerID);
            CustomerBranch del =
            CustomerBranchRepo.Remove(new CustomerBranch { ID = selectedBookedBranch.FirstOrDefault().ID });
            unitOfWork.commit();
            return del.ToEditableViewModel();
        }
        public BranchViewModel GetByID(int id)
        {
            return BranchRepo.GetByID(id).ToViewModel();
        }
        public BranchViewModel GetLatestBranch(int customerID)
        {
            return BranchRepo.GetAll().Where(i=>i.CustomerBranches.Select(a => a.CustomerID == customerID).FirstOrDefault()).
                OrderByDescending(o=>o.ID).FirstOrDefault().ToViewModel();
        }
        public IEnumerable<BranchViewModel> GetAll()
        {
            var query =
                BranchRepo.GetAll();
            return query.ToList().Select(i => i.ToViewModel());
        }
        public IEnumerable<BranchViewModel> GetLatest(int index = 0 , int CustomerID = 0)
        {
            var query =
                BranchRepo.GetAll();
            if (CustomerID > 0)
                query = query.Where(i => i.CustomerBranches.Select(a => a.CustomerID == CustomerID).FirstOrDefault());
            if (index > 0)
                query = query.OrderByDescending(i => i.ID).Take(index);
            return query.OrderByDescending(i => i.ID).ToList().Select(i => i.ToViewModel());
        }
        public PagingViewModel Get(int pageIndex = 0, int pageSize = 3
             ,int CustomerID =0,string name="" , bool isDescinding = false)
        {
            var query =
                BranchRepo.GetAll();
           if (CustomerID > 0)
                query = query.Where(i => i.CustomerBranches.Select(a => a.CustomerID == CustomerID).FirstOrDefault());
            if (isDescinding == true)
                query = query.OrderByDescending(i => i.ID);
            int records = query.Count();
            if (records <= pageSize || pageIndex <= 0) pageIndex = 1;
            int pages = (int)Math.Ceiling((double)records / pageSize);
            int excludedRows = (pageIndex - 1) * pageSize;
            if (isDescinding == false)
            query = query.OrderBy(i => i.ID);

            query = query.Skip(excludedRows).Take(pageSize);

            var data = query.ToList().Select(i => i.ToViewModel()).ToList();
            return new PagingViewModel()
            { PageIndex = pageIndex, PageSize = pageSize, Result = data, Records = records, Pages = pages };
        }
        public PagingViewModel GetBookedBranches(int CustomerID, int pageIndex = 0, int pageSize = 3)
        {
            var query =
                BranchRepo.GetAll();

            var customerBranches = CustomerBranchRepo.GetAll().Where(i => i.CustomerID == CustomerID);
            List<Branch> branches = new List<Branch>();
            foreach (var customerBranch in customerBranches)
            {
                var branch = query.Where(i => i.ID == customerBranch.BranchID).FirstOrDefault();
                branches.Add(branch);
            }
                query = branches.AsQueryable();
            
            int records = query.Count();
            if (records <= pageSize || pageIndex <= 0) pageIndex = 1;
            int pages = (int)Math.Ceiling((double)records / pageSize);
            int excludedRows = (pageIndex - 1) * pageSize;

            query = query.Skip(excludedRows).Take(pageSize);

            var data = query.ToList().Select(i => i.ToViewModel()).ToList();
            return new PagingViewModel()
            { PageIndex = pageIndex, PageSize = pageSize, Result = data, Records = records, Pages = pages };
        }


        public IEnumerable<BranchViewModel> SearchWithFilter(int pageIndex = 0, int pageSize = 3, string title = "")
        {
            var query =
                BranchRepo.GetAll();
            if (!String.IsNullOrEmpty(title))
                query = query.Where(i => i.Title == title).AsQueryable();
            int records = query.Count();
            if (records <= pageSize || pageIndex <= 0) pageIndex = 1;
            int pages = (int)Math.Ceiling((double)records / pageSize);
            int excludedRows = (pageIndex - 1) * pageSize;

            query = query.Skip(excludedRows).Take(pageSize);

            var data = query.ToList().Select(i => i.ToViewModel());
            return data;
        }

    }
}

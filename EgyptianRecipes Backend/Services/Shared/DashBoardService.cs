using Entities.Entities;
using Repositories;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace Services
{
    public class DashBoardService
    {
        UnitOfWork unitOfWork;
        Generic<Branch> BranchRepo;
        Generic<Admin> AdminRepo;
        Generic<Customer> CustomerRepo;

        public DashBoardService(UnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
            BranchRepo = unitOfWork.BranchRepo;
            AdminRepo = unitOfWork.AdminRepo;
            CustomerRepo = unitOfWork.CustomerRepo;
        }



        public DashboardViewModel GetStatistics()
        {
            DashboardViewModel report = new DashboardViewModel();
            report.TotalBranches = BranchRepo.GetAll().Count();
            report.TotalCustomers = CustomerRepo.GetAll().Count();
            report.TotalAdmins = AdminRepo.GetAll().Count();
            return report;
        }
    }
}

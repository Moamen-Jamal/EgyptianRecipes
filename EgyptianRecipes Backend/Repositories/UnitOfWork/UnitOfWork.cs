using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class UnitOfWork
    {
        private EntitiesContext context;
        
        public Generic<Customer> CustomerRepo { get; set; }
        public Generic<Branch> BranchRepo { get; set; }
        public Generic<Admin> AdminRepo { get; set; }
        public Generic<Role> RoleRepo { get; set; }
        public Generic<User> UserRepo { get; set; }
        public Generic<UserRole> UserRoleRepo { get; set; }
        public Generic<CustomerBranch> CustomerBranchRepo { get; set; }

        ////////////////////////////////

        public UnitOfWork(
            EntitiesContext _context,

            
            Generic<Customer> customerRepo,
            Generic<Branch> branchRepo,
            Generic<Admin> adminRepo,
            Generic<Role> roleRepo,
            Generic<User> userRepo,
            Generic<UserRole> userRoleRepo,
            Generic<CustomerBranch> customerBranchRepo
            )
        {
            context = _context;


            CustomerRepo = customerRepo;
            CustomerRepo.Context = context;

            BranchRepo = branchRepo;
            BranchRepo.Context = context;

            AdminRepo = adminRepo;
            AdminRepo.Context = context;

            UserRepo = userRepo;
            UserRepo.Context = context;

            RoleRepo = roleRepo;
            RoleRepo.Context = context;

            UserRoleRepo = userRoleRepo;
            UserRoleRepo.Context = context;

            CustomerBranchRepo = customerBranchRepo;
            CustomerBranchRepo.Context = context;

        }

        public int commit()
        {
            return context.SaveChanges();
        }

    }
}

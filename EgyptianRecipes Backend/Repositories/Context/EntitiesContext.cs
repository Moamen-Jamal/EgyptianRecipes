using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Repositories
{
    public class EntitiesContext : DbContext
    {
        public EntitiesContext() : base("name=EgyptianRecipesDB")
        { }
        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Branch> Branches { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<CustomerBranch> CustomerBranches { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new BranchConfiguration());
            modelBuilder.Configurations.Add(new AdminConfiguration());
            modelBuilder.Configurations.Add(new CustomerConfiguration());
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new RoleConfiguration());
            modelBuilder.Configurations.Add(new UserRoleConfiguration());
            modelBuilder.Configurations.Add(new CustomerBranchConfiguration());
            modelBuilder.Entity<User>().HasIndex(i => i.UserName).IsUnique();
            modelBuilder.Entity<User>().HasIndex(i => i.Email).IsUnique();
        }
    }
}

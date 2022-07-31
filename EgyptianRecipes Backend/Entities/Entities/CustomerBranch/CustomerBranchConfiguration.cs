using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class CustomerBranchConfiguration : EntityTypeConfiguration<CustomerBranch>
    {
        public CustomerBranchConfiguration()
        {
            ToTable("CustomerBranch");
            Property(i => i.BranchID).IsRequired();
            Property(i => i.CustomerID).IsRequired();
            HasRequired(i => i.Customer).WithMany(i => i.CustomerBranches).
                HasForeignKey(i => i.CustomerID);
            HasRequired(i => i.Branch).WithMany(i => i.CustomerBranches).
                HasForeignKey(i => i.BranchID);
        }
    }
}

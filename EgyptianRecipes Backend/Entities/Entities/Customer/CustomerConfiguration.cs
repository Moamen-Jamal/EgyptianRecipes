using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class CustomerConfiguration : EntityTypeConfiguration<Customer>
    {
        public CustomerConfiguration()
        {
            ToTable("Customer");
            Property(i => i.Gender).HasMaxLength(50).IsRequired();
            Property(i => i.BirthDate).IsRequired();
            HasRequired(i => i.User).
                WithMany().HasForeignKey(i => i.ID);
            HasMany(i => i.CustomerBranches).WithRequired(i => i.Customer);
        }
    }
}

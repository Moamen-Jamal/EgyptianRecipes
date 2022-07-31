using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class BranchConfiguration : EntityTypeConfiguration<Branch>
    {
        public BranchConfiguration()
        {
            ToTable("Branch");
            Property(i => i.Title).HasMaxLength(200).IsRequired();
            Property(i => i.ManagerName).HasMaxLength(250).IsRequired();
            Property(i => i.OpeningHour).IsRequired();
            Property(i => i.ClosingHour).IsRequired();
            HasMany(i => i.CustomerBranches).WithRequired(i => i.Branch);
        }
    }
}

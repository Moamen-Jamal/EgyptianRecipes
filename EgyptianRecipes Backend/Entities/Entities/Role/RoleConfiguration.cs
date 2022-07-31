using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class RoleConfiguration : EntityTypeConfiguration<Role>
    {
        public RoleConfiguration()
        {
            ToTable("Role");
            Property(i => i.Name).HasMaxLength(100).IsRequired();
            HasMany(i => i.UserRoles).WithRequired(i => i.Role);
        }
    }
}

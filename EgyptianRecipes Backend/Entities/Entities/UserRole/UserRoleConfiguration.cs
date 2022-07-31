using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class UserRoleConfiguration : EntityTypeConfiguration<UserRole>
    {
        public UserRoleConfiguration()
        {
            ToTable("UserRole");
            Property(i => i.RoleID).IsRequired();
            Property(i => i.UserID).IsRequired();
            HasRequired(i => i.User).WithMany(i => i.UserRoles).
                HasForeignKey(i => i.UserID);
            HasRequired(i => i.Role).WithMany(i => i.UserRoles).
                HasForeignKey(i => i.RoleID);
        }
    }
}

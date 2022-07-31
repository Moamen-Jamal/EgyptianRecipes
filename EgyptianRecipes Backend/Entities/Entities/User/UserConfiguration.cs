using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            ToTable("User");
            Property(i => i.Name).HasMaxLength(100).IsRequired();
            Property(i => i.UserName).HasMaxLength(100).IsRequired();
            Property(i => i.Password).HasMaxLength(100).IsRequired();
            Property(i => i.Phone).HasMaxLength(100).IsRequired();
            Property(i => i.Photo).HasMaxLength(150).IsRequired();
            Property(i => i.Email).HasMaxLength(100).IsRequired();
            HasMany(i => i.UserRoles).WithRequired(i => i.User);


            HasOptional(i => i.Admin)
                .WithRequired(i => i.User);

            HasOptional(i => i.Customer)
              .WithRequired(i => i.User);
        }
    }
}

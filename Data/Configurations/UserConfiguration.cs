using Data.Configurations.Base;
using Data.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    internal class UserConfiguration : AuditableEntityConfiguration<User>, IEntityTypeConfiguration<User>
    {
        public new void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);
            builder.HasMany(x => x.Roles).WithMany(x => x.Users);
            builder.ToTable("users");
        }
    }
}

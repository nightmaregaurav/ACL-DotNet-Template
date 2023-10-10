using Data.Configurations.Base;
using Data.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    internal class RoleConfiguration : AuditableEntityConfiguration<Role>, IEntityTypeConfiguration<Role>
    {
        public new void Configure(EntityTypeBuilder<Role> builder)
        {
            base.Configure(builder);
            builder.HasMany(x => x.Users).WithMany(x => x.Roles);
            builder.ToTable("roles");
        }
    }
}

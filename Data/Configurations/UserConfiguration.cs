using Data.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey("Id");
            builder.Property(x => x.Guid).IsRequired();
            builder.Property(x => x.FullName).IsRequired();
            builder.Property(x => x.Permissions).IsRequired(false);
            builder.HasMany(x => x.Roles).WithMany(x => x.Users);
            builder.ToTable("users");
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PolicyPermission.Entity.Entities;

namespace PolicyPermission.Data.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey("Id");
            builder.Property(x => x.Guid).IsRequired();
            builder.Property(x => x.FullName).IsRequired();
            
            builder.HasOne(x => x.Role).WithMany().IsRequired();
            
            builder.ToTable("users");
        }
    }
}
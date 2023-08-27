using Data.Entity.Entities;
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
            
            builder.HasOne(x => x.Role).WithMany().IsRequired();
            
            builder.ToTable("users");
        }
    }
}
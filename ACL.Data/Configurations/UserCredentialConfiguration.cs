using ACL.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ACL.Data.Configurations
{
    internal class UserCredentialConfiguration : IEntityTypeConfiguration<UserCredential>
    {
        public void Configure(EntityTypeBuilder<UserCredential> builder)
        {
            builder.HasKey("Id");
            builder.Property(x => x.Guid).IsRequired();
            builder.Property(x => x.UserName).IsRequired();
            builder.Property(x => x.Password).IsRequired();
            
            builder.HasOne(x => x.User).WithMany().IsRequired();
            
            builder.ToTable("user_credentials");
        }
    }
}
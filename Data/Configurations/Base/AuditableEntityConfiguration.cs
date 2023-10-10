using Data.Models.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations.Base
{
    internal abstract class AuditableEntityConfiguration<T> : EntityConfiguration<T>, IEntityTypeConfiguration<T> where T : AuditableEntity
    {
        public new void Configure(EntityTypeBuilder<T> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.LastUpdatedAt).IsRequired();

            builder.HasOne(x => x.CreatedBy).WithMany().HasForeignKey();
            builder.HasOne(x => x.LastUpdatedBy).WithMany().HasForeignKey();
        }
    }
}

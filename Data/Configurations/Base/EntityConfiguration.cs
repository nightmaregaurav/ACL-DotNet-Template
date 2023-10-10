using Data.Models.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations.Base
{
    internal abstract class EntityConfiguration<T> : IEntityTypeConfiguration<T> where T : EntityBase
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey("Id");
            builder.Property("Id").IsRequired();
            builder.Property(x => x.Guid).IsRequired();
            builder.Property(x => x.IsDeleted).IsRequired();
        }
    }
}

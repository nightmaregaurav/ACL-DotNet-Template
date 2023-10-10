using Data.Models.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations.Base
{
    internal abstract class KeylessEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : KeylessEntity
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasNoKey();
            builder.Property(x => x.IsDeleted).IsRequired();
        }
    }
}

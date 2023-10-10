using Data.Models.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations.Base
{
    internal class AuditEntryConfiguration : EntityConfiguration<AuditEntry>, IEntityTypeConfiguration<AuditEntry>
    {
        public new void Configure(EntityTypeBuilder<AuditEntry> builder)
        {
            base.Configure(builder);
            builder.ToTable("audit_log");

            builder.Property(x => x.OldValues).HasColumnType("jsonb");
            builder.Property(x => x.NewValues).HasColumnType("jsonb");
        }
    }
}

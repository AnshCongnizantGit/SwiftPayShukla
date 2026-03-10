using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SwiftPay.Domain.Remittance.Entities;

namespace SwiftPay.Config.Configuration
{
    public class AmendmentConfiguration : IEntityTypeConfiguration<Amendment>
    {
        public void Configure(EntityTypeBuilder<Amendment> builder)
        {
            builder.Property(a => a.AmendmentId).HasMaxLength(64);
            builder.Property(a => a.RemitId).IsRequired().HasMaxLength(64);
            builder.Property(a => a.FieldChanged).IsRequired().HasMaxLength(100);
            builder.Property(a => a.RequestedByUserId).IsRequired().HasMaxLength(64);
            builder.Property(a => a.RequestedDate).IsRequired().HasDefaultValueSql("GETUTCDATE()");
            builder.Property(a => a.Status).HasMaxLength(50).HasDefaultValue("Pending");
            builder.Property(a => a.CreatedDate).IsRequired().HasDefaultValueSql("GETUTCDATE()");
            builder.Property(a => a.CreatedByUserId).HasMaxLength(64);
            builder.Property(a => a.UpdatedByUserId).HasMaxLength(64);
            builder.Property(a => a.RowVersion).HasDefaultValue(Array.Empty<byte>());
        }
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SwiftPay.Domain.Remittance.Entities;

namespace SwiftPay.Config.Configuration
{
    public class ComplianceScreeningConfiguration : IEntityTypeConfiguration<ComplianceCheck>
    {
        public void Configure(EntityTypeBuilder<ComplianceCheck> builder)
        {
            builder.HasKey(c => c.CheckId);
            builder.Property(c => c.CheckId).HasMaxLength(64);
            builder.Property(c => c.RemitId).IsRequired().HasMaxLength(64);
            builder.Property(c => c.CheckType).IsRequired().HasMaxLength(32);
            builder.Property(c => c.Result).IsRequired().HasMaxLength(20);
            builder.Property(c => c.Severity).IsRequired().HasMaxLength(20);
            builder.Property(c => c.CheckedDate).IsRequired().HasDefaultValueSql("GETUTCDATE()");
            builder.Property(c => c.RowVersion).IsRowVersion().HasDefaultValue(Array.Empty<byte>());
        }
    }
}
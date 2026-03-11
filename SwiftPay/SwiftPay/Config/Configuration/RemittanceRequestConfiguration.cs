using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SwiftPay.Domain.Remittance.Entities;

namespace SwiftPay.Config.Configuration
{
    public class RemittanceRequestConfiguration : IEntityTypeConfiguration<RemittanceRequest>
    {
        public void Configure(EntityTypeBuilder<RemittanceRequest> builder)
        {
            builder.HasKey(r => r.RemitId);
            builder.Property(r => r.RemitId).HasMaxLength(64);
            builder.Property(r => r.CustomerId).IsRequired().HasMaxLength(64);
            builder.Property(r => r.BeneficiaryId).IsRequired().HasMaxLength(64);
            builder.Property(r => r.FromCurrency).IsRequired().HasMaxLength(3);
            builder.Property(r => r.ToCurrency).IsRequired().HasMaxLength(3);
            builder.Property(r => r.SendAmount).IsRequired().HasPrecision(18, 2);
            builder.Property(r => r.ReceiverAmount).HasPrecision(18, 2);
            builder.Property(r => r.QuoteId).HasMaxLength(64);
            builder.Property(r => r.RateLockId).HasMaxLength(64);
            builder.Property(r => r.RateApplied).HasPrecision(18, 6);
            builder.Property(r => r.FeeApplied).HasPrecision(18, 2);
            builder.Property(r => r.PurposeCode).HasMaxLength(32);
            builder.Property(r => r.SourceOfFunds).HasMaxLength(64);
            builder.Property(r => r.CreatedDate).IsRequired().HasDefaultValueSql("GETUTCDATE()");
            builder.Property(r => r.CreatedByUserId).HasMaxLength(64);
            builder.Property(r => r.UpdatedByUserId).HasMaxLength(64);
            builder.Property(r => r.RowVersion).IsRowVersion().HasDefaultValue(Array.Empty<byte>());
        }
    }
}
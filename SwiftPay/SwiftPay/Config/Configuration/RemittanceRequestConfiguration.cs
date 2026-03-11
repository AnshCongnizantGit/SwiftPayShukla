using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RemittanceModule;
using SwiftPay.Constants.Enums;

namespace SwiftPay.Config.Configuration
{
	public class RemittanceRequestConfiguration : IEntityTypeConfiguration<RemittanceRequest>
	{
		public void Configure(EntityTypeBuilder<RemittanceRequest> builder)
		{
			// Table Mapping
			builder.ToTable("RemittanceRequests");

			// Primary Key
			builder.HasKey(r => r.RemitId);

			// Required Strings & Length Constraints
			builder.Property(r => r.FromCurrency)
				.IsRequired()
				.HasMaxLength(3)
				.IsFixedLength(); // Standard for ISO currency codes

			builder.Property(r => r.ToCurrency)
				.IsRequired()
				.HasMaxLength(3)
				.IsFixedLength();

			builder.Property(r => r.QuoteId)
				.IsRequired()
				.HasMaxLength(100);

			builder.Property(r => r.PurposeCode)
				.IsRequired()
				.HasMaxLength(50);

			builder.Property(r => r.SourceOfFunds)
				.IsRequired()
				.HasMaxLength(100);

			// Decimal Precision (Crucial for Forex/Finance)
			// 18,2 for amounts; 18,6 or higher for exchange rates
			builder.Property(r => r.SendAmount)
				.HasPrecision(18, 2);

			builder.Property(r => r.ReceiverAmount)
				.HasPrecision(18, 2);

			builder.Property(r => r.FeeApplied)
				.HasPrecision(18, 2);

			builder.Property(r => r.RateApplied)
				.HasPrecision(18, 8); // Higher precision for FX rates

			// Date & Enum Defaults
			builder.Property(r => r.CreatedDate)
				.HasDefaultValueSql("GETUTCDATE()");

			builder.Property(r => r.UpdateDate)
				.HasDefaultValueSql("GETUTCDATE()");

			builder.Property(r => r.Status)
				.HasConversion<string>() // Stores enum as integer in DB
				.HasDefaultValue(RemittanceRequestStatus.Draft);

			// Indices for performance
			builder.HasIndex(r => r.CustomerId);
			builder.HasIndex(r => r.QuoteId).IsUnique();
		}
	}
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SwiftPay.FXModule.Api.Models;

namespace SwiftPay.Config.Configuration
{
    public class FXQuoteConfiguration : IEntityTypeConfiguration<FXQuote>
    {
        public void Configure(EntityTypeBuilder<FXQuote> builder)
        {
            builder.HasKey(f => f.QuoteID);
            builder.Property(f => f.QuoteID).HasDefaultValueSql("NEWID()");
            builder.Property(f => f.FromCurrency).IsRequired().HasDefaultValue(string.Empty);
            builder.Property(f => f.ToCurrency).IsRequired().HasDefaultValue(string.Empty);
            builder.Property(f => f.QuoteTime).HasDefaultValueSql("GETUTCDATE()");
            builder.Property(f => f.Status).IsRequired().HasDefaultValue("Active");
        }
    }
}
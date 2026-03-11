using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SwiftPay.Constants.Enums;
using SwiftPay.Domain.Remittance.Entities;

namespace SwiftPay.Config.Configuration
{
    public class AmendmentModelConfiguration :
        IEntityTypeConfiguration<Amendment>,
        IEntityTypeConfiguration<Cancellation>,
        IEntityTypeConfiguration<RefundRef>
    {
        public void Configure(EntityTypeBuilder<Amendment> builder)
        {
            builder.ToTable("Amendments");
            builder.HasKey(a => a.AmendmentID);
            
            builder.Property(a => a.FieldChanged).IsRequired().HasMaxLength(100);
            builder.Property(a => a.OldValue).HasColumnType("text");
            builder.Property(a => a.NewValue).HasColumnType("text");
            
            builder.Property(a => a.Status)
                .HasConversion<string>()
                .HasMaxLength(50)
                .HasDefaultValue(AmendmentStatus.Pending);

            builder.Property(a => a.RequestedDate).HasDefaultValueSql("GETUTCDATE()");
            builder.Property(a => a.CreatedDate).HasDefaultValueSql("GETUTCDATE()");
            builder.Property(a => a.IsDeleted).HasDefaultValue(false);
        }

        public void Configure(EntityTypeBuilder<Cancellation> builder)
        {
            builder.ToTable("Cancellations");
            builder.HasKey(c => c.CancellationID);
            builder.Property(c => c.Reason).IsRequired().HasColumnType("text");
            
            builder.Property(c => c.Status)
                .HasConversion<string>()
                .HasMaxLength(50)
                .HasDefaultValue(CancellationStatus.Requested);

            builder.Property(c => c.RequestedDate).HasDefaultValueSql("GETUTCDATE()");
            builder.Property(c => c.CreatedDate).HasDefaultValueSql("GETUTCDATE()");
            builder.Property(c => c.IsDeleted).HasDefaultValue(false);
        }

        public void Configure(EntityTypeBuilder<RefundRef> builder)
        {
            builder.ToTable("RefundRefs");
            builder.HasKey(r => r.RefundID);
            builder.Property(r => r.Amount).HasPrecision(15, 4);
            
            builder.Property(r => r.Method)
                .HasConversion<string>()
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(r => r.Status)
                .HasConversion<string>()
                .HasMaxLength(50)
                .HasDefaultValue(RefundStatus.Initiated);

            builder.Property(r => r.CreatedDate).HasDefaultValueSql("GETUTCDATE()");
            builder.Property(r => r.IsDeleted).HasDefaultValue(false);
        }
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SwiftPay.Domain.Remittance.Entities;

namespace SwiftPay.Config.Configuration
{
    public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
    {
        public void Configure(EntityTypeBuilder<AuditLog> builder)
        {
            builder.HasKey(a => a.AuditID);
            builder.Property(a => a.AuditID).ValueGeneratedOnAdd();

            builder.Property(a => a.UserID)
                .IsRequired();

            builder.Property(a => a.Action)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(a => a.Resource)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(a => a.Timestamp)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            // CreatedAt with default current timestamp
            builder.Property(a => a.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            // UpdatedAt with default current timestamp
            builder.Property(a => a.UpdatedAt)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            // IsDeleted with default value false
            builder.Property(a => a.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false);

            // Create index on UserID for faster queries
            builder.HasIndex(a => a.UserID);

            // Create index on Resource for faster queries
            builder.HasIndex(a => a.Resource);

            // Create composite index on Timestamp and UserID for date range queries
            builder.HasIndex(a => new { a.Timestamp, a.UserID });

            // Foreign key to User - Restrict delete to preserve audit trail
            builder.HasOne(a => a.User)
                .WithMany()
                .HasForeignKey(a => a.UserID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

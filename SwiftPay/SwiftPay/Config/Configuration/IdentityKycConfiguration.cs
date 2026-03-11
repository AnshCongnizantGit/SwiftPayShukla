using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model;

namespace SwiftPay.Config.Configuration
{
    public class IdentityKycConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.UserId);
            builder.Property(u => u.Name).IsRequired().HasDefaultValue(string.Empty);
            builder.Property(u => u.Email).IsRequired().HasDefaultValue(string.Empty);
            builder.Property(u => u.Phone).IsRequired().HasDefaultValue(string.Empty);
        }
    }
}
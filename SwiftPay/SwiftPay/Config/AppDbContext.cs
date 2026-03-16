using Microsoft.EntityFrameworkCore;
using SwiftPay.Config.Configuration;

namespace SwiftPay.Configuration
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        // DbSets for remittance module
        public DbSet<Domain.Remittance.Entities.RemittanceRequest> RemittanceRequests { get; set; }
        public DbSet<Models.RemitValidation> RemitValidations { get; set; }
        public DbSet<Models.Document> RemittanceDocuments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}

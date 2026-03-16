using System.Threading.Tasks;
using SwiftPay.Repositories.Interfaces;
using SwiftPay.Domain.Remittance.Entities;
using SwiftPay.Configuration;

namespace SwiftPay.Repositories
{
    public class RemittanceRepository : IRemittanceRepository
    {
        private readonly AppDbContext _db;

        public RemittanceRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<RemittanceRequest> CreateAsync(RemittanceRequest entity)
        {
            await _db.Set<RemittanceRequest>().AddAsync(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}

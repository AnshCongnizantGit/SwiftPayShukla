using Microsoft.EntityFrameworkCore;
using SwiftPay.Domain.Remittance.Entities;
using SwiftPay.Repositories.Interfaces;
using SwiftPay.Configuration; // Assuming AppDbContext is here

namespace SwiftPay.Repositories
{
    public class RoutingRuleRepository : IRoutingRuleRepository
    {
        private readonly AppDbContext _db;
        public RoutingRuleRepository(AppDbContext db) => _db = db;

        public async Task<RoutingRule> AddAsync(RoutingRule entity)
        {
            await _db.Set<RoutingRule>().AddAsync(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<RoutingRule?> GetByIdAsync(string id) =>
            await _db.Set<RoutingRule>().FirstOrDefaultAsync(x => x.RuleId == id && !x.IsDeleted);

        public async Task<IEnumerable<RoutingRule>> GetAllAsync() =>
            await _db.Set<RoutingRule>().Where(x => !x.IsDeleted).ToListAsync();

        public async Task UpdateAsync(RoutingRule entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                entity.IsDeleted = true;
                entity.UpdateDate = DateTimeOffset.UtcNow;
                await _db.SaveChangesAsync();
            }
        }
    }
}
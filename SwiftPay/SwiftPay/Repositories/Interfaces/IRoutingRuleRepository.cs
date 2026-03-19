using SwiftPay.Domain.Remittance.Entities;

namespace SwiftPay.Repositories.Interfaces
{
    public interface IRoutingRuleRepository
    {
        Task<RoutingRule> AddAsync(RoutingRule entity);
        Task<RoutingRule?> GetByIdAsync(string id);
        Task<IEnumerable<RoutingRule>> GetAllAsync();
        Task UpdateAsync(RoutingRule entity);
        Task DeleteAsync(string id);
    }
}

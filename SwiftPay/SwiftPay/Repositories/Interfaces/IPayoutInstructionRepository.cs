using SwiftPay.Domain.Remittance.Entities;

namespace SwiftPay.Repositories.Interfaces
{
    public interface IPayoutInstructionRepository
    {
        Task<PayoutInstruction> AddAsync(PayoutInstruction entity);
        Task<PayoutInstruction?> GetByIdAsync(string id);
        Task<IEnumerable<PayoutInstruction>> GetAllAsync();
        Task UpdateAsync(PayoutInstruction entity);
        Task DeleteAsync(string id);
    }
}
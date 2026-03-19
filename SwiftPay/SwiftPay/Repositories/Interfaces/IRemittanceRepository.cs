using System.Threading.Tasks;
using SwiftPay.Domain.Remittance.Entities;

namespace SwiftPay.Repositories.Interfaces
{
    public interface IRemittanceRepository
    {
        Task<RemittanceRequest> CreateAsync(RemittanceRequest entity);

        // Get by string RemitId (GUID stored as string)
        Task<RemittanceRequest?> GetByIdAsync(string remitId);
        
        // Update an existing remittance
        Task UpdateAsync(RemittanceRequest entity);
    }
}

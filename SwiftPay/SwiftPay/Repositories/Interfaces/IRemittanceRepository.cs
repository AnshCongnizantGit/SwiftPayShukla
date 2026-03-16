using System.Threading.Tasks;
using SwiftPay.Domain.Remittance.Entities;

namespace SwiftPay.Repositories.Interfaces
{
    public interface IRemittanceRepository
    {
        Task<RemittanceRequest> CreateAsync(RemittanceRequest entity);
    }
}

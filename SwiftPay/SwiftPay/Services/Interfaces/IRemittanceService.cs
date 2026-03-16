using System.Threading.Tasks;
using SwiftPay.DTOs.RemittanceDTO;
using SwiftPay.Domain.Remittance.Entities;

namespace SwiftPay.Services.Interfaces
{
    public interface IRemittanceService
    {
        Task<RemittanceRequest> CreateAsync(CreateRemittanceDto dto);
    }
}

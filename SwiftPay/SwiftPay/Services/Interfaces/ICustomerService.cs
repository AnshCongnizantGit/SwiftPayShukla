using System.Collections.Generic;
using System.Threading.Tasks;
using SwiftPay.DTOs.UserCustomerDTO;

namespace SwiftPay.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerResponseDto> CreateAsync(CreateCustomerDto dto);
        Task<CustomerResponseDto> GetByIdAsync(int customerId);
        Task<CustomerResponseDto> GetByUserIdAsync(int userId);
        Task<IEnumerable<CustomerResponseDto>> GetAllAsync();
        Task<CustomerResponseDto> UpdateAsync(int customerId, UpdateCustomerDto dto);
        Task<CustomerResponseDto> UpdateRiskRatingAsync(int customerId, UpdateCustomerRiskRatingDto dto);
        Task<bool> DeleteAsync(int customerId);
    }
}

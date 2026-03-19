using System.Collections.Generic;
using System.Threading.Tasks;
using SwiftPay.DTOs.UserCustomerDTO;

namespace SwiftPay.Services.Interfaces
{
    public interface IBeneficiaryService
    {
        Task<BeneficiaryResponseDto> CreateAsync(CreateBeneficiaryDto dto);
        Task<BeneficiaryResponseDto> GetByIdAsync(int beneficiaryId);
        Task<IEnumerable<BeneficiaryResponseDto>> GetByCustomerIdAsync(int customerId);
        Task<IEnumerable<BeneficiaryResponseDto>> GetAllAsync();
        Task<BeneficiaryResponseDto> UpdateAsync(int beneficiaryId, UpdateBeneficiaryDto dto);
        Task<BeneficiaryResponseDto> UpdateVerificationStatusAsync(int beneficiaryId, UpdateBeneficiaryVerificationStatusDto dto);
        Task<bool> DeleteAsync(int beneficiaryId);
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using SwiftPay.DTOs.UserCustomerDTO;

namespace SwiftPay.Services.Interfaces
{
    public interface IKYCRecordService
    {
        Task<KYCRecordResponseDto> CreateAsync(CreateKYCRecordDto dto);
        Task<KYCRecordResponseDto> GetByIdAsync(int kycId);
        Task<KYCRecordResponseDto> GetByUserIdAsync(int userId);
        Task<IEnumerable<KYCRecordResponseDto>> GetAllAsync();
        Task<KYCRecordResponseDto> UpdateAsync(int kycId, UpdateKYCRecordDto dto);
        Task<KYCRecordResponseDto> UpdateStatusAsync(int kycId, UpdateKycStatusDto dto);
        Task<KYCRecordResponseDto> MarkAsVerifiedAsync(int kycId);
        Task<KYCRecordListDto> GetPendingAsync(int pageNumber = 1, int pageSize = 10);
        Task<bool> DeleteAsync(int kycId);
    }
}

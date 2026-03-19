using SwiftPay.Constants.Enums;
using SwiftPay.DTOs.PayoutDTO;

namespace SwiftPay.Services.Interfaces
{
    public interface IPayoutInstructionService
    {
        Task<bool> UpdateAsync(string id, CreatePayoutInstructionDto dto);
        Task<PayoutInstructionResponseDto> CreateInstructionAsync(CreatePayoutInstructionDto dto);
        Task<PayoutInstructionResponseDto?> GetByIdAsync(string id);
        Task<IEnumerable<PayoutInstructionResponseDto>> GetAllAsync();
        Task<bool> UpdateStatusAsync(string id, PayOutInstructionStatus status, string ackRef);
        Task<bool> DeleteAsync(string id);
    }
}
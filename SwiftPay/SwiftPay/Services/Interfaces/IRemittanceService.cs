using System.Threading.Tasks;
using SwiftPay.DTOs.RemittanceDTO;

namespace SwiftPay.Services.Interfaces
{
    public interface IRemittanceService
    {
        Task<CreateRemittanceResponseDto> CreateAsync(CreateRemittanceDto dto);

        // Get by string RemitId (GUID stored as string)
        Task<CreateRemittanceResponseDto?> GetByIdAsync(string remitId);


        // Remit validation functions
		Task<ValidateRemittanceResponseDto> ValidateAsync(string remitId);

		Task<List<RemitValidationDto>> GetValidationsAsync(string remitId);

        // Update and delete remittance
        Task UpdateAsync(string remitId, CreateRemittanceDto dto);
        Task<bool> DeleteAsync(string remitId);

        // Update and delete individual validation records
        Task UpdateValidationAsync(RemitValidationDto dto);
        Task DeleteValidationAsync(Guid validationId);

        //Document functions 


	}
}

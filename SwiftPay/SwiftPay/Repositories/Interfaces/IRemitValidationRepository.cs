using System.Collections.Generic;
using System.Threading.Tasks;
using SwiftPay.Models;

namespace SwiftPay.Repositories.Interfaces
{
    public interface IRemitValidationRepository
    {
        Task AddRangeAsync(IEnumerable<RemitValidation> validations);
        Task<List<RemitValidation>> GetByRemitIdAsync(string remitId);
        Task<RemitValidation?> GetByIdAsync(Guid validationId);
        Task UpdateAsync(RemitValidation validation);
        Task<bool> DeleteAsync(Guid validationId);
    }
}

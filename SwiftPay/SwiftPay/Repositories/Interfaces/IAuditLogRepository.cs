using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SwiftPay.Domain.Remittance.Entities;

namespace SwiftPay.Repositories.Interfaces
{
    public interface IAuditLogRepository
    {
        Task<AuditLog> CreateAsync(AuditLog entity);
        Task<AuditLog> GetByIdAsync(int auditId);
        Task<IEnumerable<AuditLog>> GetByUserIdAsync(int userId);
        Task<IEnumerable<AuditLog>> GetByResourceAsync(string resource);
        Task<IEnumerable<AuditLog>> GetAllAsync();
        Task<IEnumerable<AuditLog>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
        
        // Advanced filtering with multiple criteria
        Task<(IEnumerable<AuditLog> logs, int totalCount)> GetFilteredAsync(
            int? userId = null,
            string resource = null,
            DateTime? startDate = null,
            DateTime? endDate = null,
            int pageNumber = 1,
            int pageSize = 20);
        
        Task<bool> DeleteAsync(int auditId);
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SwiftPay.Repositories.Interfaces;
using SwiftPay.Configuration;
using SwiftPay.Domain.Remittance.Entities;

namespace SwiftPay.Repositories
{
    public class AuditLogRepository : IAuditLogRepository
    {
        private readonly AppDbContext _db;

        public AuditLogRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<AuditLog> CreateAsync(AuditLog entity)
        {
            await _db.Set<AuditLog>().AddAsync(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<AuditLog> GetByIdAsync(int auditId)
        {
            return await _db.Set<AuditLog>()
                .Include(a => a.User)
                .FirstOrDefaultAsync(a => a.AuditID == auditId && !a.IsDeleted);
        }

        public async Task<IEnumerable<AuditLog>> GetByUserIdAsync(int userId)
        {
            return await _db.Set<AuditLog>()
                .Include(a => a.User)
                .Where(a => a.UserID == userId && !a.IsDeleted)
                .OrderByDescending(a => a.Timestamp)
                .ToListAsync();
        }

        public async Task<IEnumerable<AuditLog>> GetByResourceAsync(string resource)
        {
            return await _db.Set<AuditLog>()
                .Include(a => a.User)
                .Where(a => a.Resource == resource && !a.IsDeleted)
                .OrderByDescending(a => a.Timestamp)
                .ToListAsync();
        }

        public async Task<IEnumerable<AuditLog>> GetAllAsync()
        {
            return await _db.Set<AuditLog>()
                .Include(a => a.User)
                .Where(a => !a.IsDeleted)
                .OrderByDescending(a => a.Timestamp)
                .ToListAsync();
        }

        public async Task<IEnumerable<AuditLog>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _db.Set<AuditLog>()
                .Include(a => a.User)
                .Where(a => a.Timestamp >= startDate && a.Timestamp <= endDate && !a.IsDeleted)
                .OrderByDescending(a => a.Timestamp)
                .ToListAsync();
        }

        public async Task<(IEnumerable<AuditLog> logs, int totalCount)> GetFilteredAsync(
            int? userId = null,
            string? resource = null,
            DateTime? startDate = null,
            DateTime? endDate = null,
            int pageNumber = 1,
            int pageSize = 20)
        {
            var query = _db.Set<AuditLog>()
                .Include(a => a.User)
                .Where(a => !a.IsDeleted);

            // Apply UserId filter if provided
            if (userId.HasValue)
                query = query.Where(a => a.UserID == userId.Value);

            // Apply Resource filter if provided
            if (!string.IsNullOrWhiteSpace(resource))
                query = query.Where(a => a.Resource.Contains(resource));

            // Apply StartDate filter if provided
            if (startDate.HasValue)
                query = query.Where(a => a.Timestamp >= startDate.Value);

            // Apply EndDate filter if provided
            if (endDate.HasValue)
                query = query.Where(a => a.Timestamp <= endDate.Value);

            // Get total count before pagination
            var totalCount = await query.CountAsync();

            // Apply pagination
            var logs = await query
                .OrderByDescending(a => a.Timestamp)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (logs, totalCount);
        }

        public async Task<bool> DeleteAsync(int auditId)
        {
            var auditLog = await GetByIdAsync(auditId);
            if (auditLog == null)
                return false;

            auditLog.IsDeleted = true;
            auditLog.UpdatedAt = DateTime.UtcNow;
            await _db.SaveChangesAsync();
            return true;
        }
    }
}

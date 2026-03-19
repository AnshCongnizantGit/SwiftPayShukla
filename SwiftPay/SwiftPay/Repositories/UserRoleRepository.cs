using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SwiftPay.Repositories.Interfaces;
using SwiftPay.Configuration;
using SwiftPay.Models;

namespace SwiftPay.Repositories
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly AppDbContext _db;

        public UserRoleRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<UserRole> CreateAsync(UserRole entity)
        {
            await _db.Set<UserRole>().AddAsync(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<UserRole> GetByIdAsync(int userRoleId)
        {
            return await _db.Set<UserRole>()
                .Include(ur => ur.User)
                .Include(ur => ur.Role)
                .FirstOrDefaultAsync(ur => ur.UserRoleId == userRoleId && !ur.IsDeleted);
        }

        public async Task<IEnumerable<UserRole>> GetByUserIdAsync(int userId)
        {
            return await _db.Set<UserRole>()
                .Include(ur => ur.Role)
                .Where(ur => ur.UserId == userId && !ur.IsDeleted)
                .ToListAsync();
        }

        public async Task<IEnumerable<UserRole>> GetByRoleIdAsync(int roleId)
        {
            return await _db.Set<UserRole>()
                .Include(ur => ur.User)
                .Where(ur => ur.RoleId == roleId && !ur.IsDeleted)
                .ToListAsync();
        }

        public async Task<UserRole> GetUserRoleAsync(int userId, int roleId)
        {
            return await _db.Set<UserRole>()
                .Include(ur => ur.User)
                .Include(ur => ur.Role)
                .FirstOrDefaultAsync(ur => ur.UserId == userId && ur.RoleId == roleId && !ur.IsDeleted);
        }

        public async Task<IEnumerable<UserRole>> GetAllAsync()
        {
            return await _db.Set<UserRole>()
                .Include(ur => ur.User)
                .Include(ur => ur.Role)
                .Where(ur => !ur.IsDeleted)
                .ToListAsync();
        }

        public async Task<UserRole> UpdateAsync(UserRole entity)
        {
            _db.Set<UserRole>().Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int userRoleId)
        {
            var userRole = await GetByIdAsync(userRoleId);
            if (userRole == null)
                return false;

            userRole.IsDeleted = true;
            userRole.UpdatedAt = DateTime.UtcNow;
            await _db.SaveChangesAsync();
            return true;
        }
    }
}

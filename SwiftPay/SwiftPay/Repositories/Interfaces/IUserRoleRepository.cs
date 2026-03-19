using System.Collections.Generic;
using System.Threading.Tasks;
using SwiftPay.Models;

namespace SwiftPay.Repositories.Interfaces
{
    public interface IUserRoleRepository
    {
        Task<UserRole> CreateAsync(UserRole entity);
        Task<UserRole> GetByIdAsync(int userRoleId);
        Task<IEnumerable<UserRole>> GetByUserIdAsync(int userId);
        Task<IEnumerable<UserRole>> GetByRoleIdAsync(int roleId);
        Task<UserRole> GetUserRoleAsync(int userId, int roleId);
        Task<IEnumerable<UserRole>> GetAllAsync();
        Task<UserRole> UpdateAsync(UserRole entity);
        Task<bool> DeleteAsync(int userRoleId);
    }
}

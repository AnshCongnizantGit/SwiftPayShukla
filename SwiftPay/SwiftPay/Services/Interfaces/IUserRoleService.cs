using System.Collections.Generic;
using System.Threading.Tasks;
using SwiftPay.DTOs.UserRoleDTO;
using SwiftPay.Models;

namespace SwiftPay.Services.Interfaces
{
    public interface IUserRoleService
    {
        Task<UserRole> AssignRoleToUserAsync(int userId, CreateUserRoleRequestDto dto);
        Task<UserRole> GetByIdAsync(int userRoleId);
        Task<IEnumerable<UserRole>> GetByUserIdAsync(int userId);
        Task<IEnumerable<UserRole>> GetByRoleIdAsync(int roleId);
        Task<bool> RemoveRoleFromUserAsync(int userRoleId);
    }
}

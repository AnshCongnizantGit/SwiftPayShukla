using System.Collections.Generic;
using System.Threading.Tasks;
using SwiftPay.DTOs.UserRoleDTO;

namespace SwiftPay.Services.Interfaces
{
    public interface IRoleService
    {
        Task<RoleResponseDto> CreateAsync(CreateRoleRequestDto dto);
        Task<RoleResponseDto> GetByIdAsync(int roleId);
        Task<IEnumerable<RoleResponseDto>> GetAllAsync();
        Task<bool> DeleteAsync(int roleId);
    }
}

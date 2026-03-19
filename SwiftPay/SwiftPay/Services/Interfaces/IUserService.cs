using System.Collections.Generic;
using System.Threading.Tasks;
using SwiftPay.DTOs.UserCustomerDTO;

namespace SwiftPay.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserResponseDto> CreateAsync(CreateUserDto dto);
        Task<UserResponseDto> GetByIdAsync(int userId);
        Task<UserResponseDto> GetByEmailAsync(string email);
        Task<IEnumerable<UserResponseDto>> GetAllAsync();
        Task<UserResponseDto> UpdateAsync(int userId, UpdateUserDto dto);
        Task<bool> DeleteAsync(int userId);
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using SwiftPay.Services.Interfaces;
using SwiftPay.Repositories.Interfaces;
using SwiftPay.Models;
using SwiftPay.DTOs.UserRoleDTO;

namespace SwiftPay.Services
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IUserRoleRepository _userRoleRepo;
        private readonly IUserRepository _userRepo;
        private readonly IRoleRepository _roleRepo;
        private readonly IMapper _mapper;

        public UserRoleService(IUserRoleRepository userRoleRepo, IUserRepository userRepo, IRoleRepository roleRepo, IMapper mapper)
        {
            _userRoleRepo = userRoleRepo;
            _userRepo = userRepo;
            _roleRepo = roleRepo;
            _mapper = mapper;
        }

        public async Task<UserRole> AssignRoleToUserAsync(int userId, CreateUserRoleRequestDto dto)
        {
            // Validate that User exists - BUSINESS LOGIC
            var user = await _userRepo.GetByIdAsync(userId);
            if (user == null)
                throw new KeyNotFoundException($"User with ID {userId} does not exist.");

            // Validate that Role exists - BUSINESS LOGIC
            var role = await _roleRepo.GetByIdAsync(dto.RoleId);
            if (role == null)
                throw new KeyNotFoundException($"Role with ID {dto.RoleId} does not exist.");

            // Check if user already has this role - BUSINESS LOGIC (prevent duplicates)
            var existingUserRole = await _userRoleRepo.GetUserRoleAsync(userId, dto.RoleId);
            if (existingUserRole != null)
                throw new InvalidOperationException($"User with ID {userId} already has the role with ID {dto.RoleId}.");

            // Use AutoMapper to map DTO to entity with userId context
            var userRole = _mapper.Map<UserRole>((userId, dto));
            // AuditLogInterceptor will automatically set CreatedAt, UpdatedAt timestamps

            var assigned = await _userRoleRepo.CreateAsync(userRole);
            return assigned;
        }

        public async Task<UserRole> GetByIdAsync(int userRoleId)
        {
            return await _userRoleRepo.GetByIdAsync(userRoleId);
        }

        public async Task<IEnumerable<UserRole>> GetByUserIdAsync(int userId)
        {
            return await _userRoleRepo.GetByUserIdAsync(userId);
        }

        public async Task<IEnumerable<UserRole>> GetByRoleIdAsync(int roleId)
        {
            return await _userRoleRepo.GetByRoleIdAsync(roleId);
        }

        public async Task<bool> RemoveRoleFromUserAsync(int userRoleId)
        {
            return await _userRoleRepo.DeleteAsync(userRoleId);
        }
    }
}

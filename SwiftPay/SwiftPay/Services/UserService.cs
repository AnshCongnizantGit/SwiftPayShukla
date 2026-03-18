using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using SwiftPay.Services.Interfaces;
using SwiftPay.Repositories.Interfaces;
using SwiftPay.DTOs.UserCustomerDTO;
using SwiftPay.Constants.Enums;
using SwiftPay.Models;

namespace SwiftPay.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;
        private readonly IMapper _mapper;

        public UserService(IUserRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<User> CreateAsync(CreateUserDto dto)
        {
            // Check if email already exists
            var existingEmail = await _repo.GetByEmailAsync(dto.Email);
            if (existingEmail != null)
                throw new Exception($"Email '{dto.Email}' is already registered. Please use a different email address.");

            // Use AutoMapper to map DTO to entity
            var entity = _mapper.Map<User>(dto);

            // Audit fields and Status are controlled by database configuration
            // DB defaults: CreatedAt, UpdatedAt (CURRENT_TIMESTAMP), IsDeleted (false), Status (Active)

            var created = await _repo.CreateAsync(entity);
            return created;
        }

        public async Task<User> GetByIdAsync(int userId)
        {
            return await _repo.GetByIdAsync(userId);
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _repo.GetByEmailAsync(email);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<User> UpdateAsync(int userId, UpdateUserDto dto)
        {
            var user = await _repo.GetByIdAsync(userId);
            if (user == null)
                throw new Exception($"User with ID {userId} not found");

            // Check email uniqueness when updating email
            if (!string.IsNullOrEmpty(dto.Email) && dto.Email != user.Email)
            {
                var existingEmail = await _repo.GetByEmailAsync(dto.Email);
                if (existingEmail != null)
                    throw new Exception($"Email '{dto.Email}' is already in use by another user. Please use a different email address.");
            }

            // Check phone uniqueness when updating phone
            if (!string.IsNullOrEmpty(dto.Phone) && dto.Phone != user.Phone)
            {
                var existingPhone = await _repo.GetByPhoneAsync(dto.Phone);
                if (existingPhone != null)
                    throw new Exception($"Phone number '{dto.Phone}' is already in use by another user. Please use a different phone number.");
            }

            // Use AutoMapper to map only non-null fields (mapper handles conditional logic)
            _mapper.Map(dto, user);

            // Update the UpdatedAt timestamp
            user.UpdatedAt = DateTime.UtcNow;

            var updated = await _repo.UpdateAsync(user);
            return updated;
        }

        public async Task<bool> DeleteAsync(int userId)
        {
            return await _repo.DeleteAsync(userId);
        }
    }
}

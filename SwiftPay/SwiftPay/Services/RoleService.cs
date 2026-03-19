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
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _repo;
        private readonly IMapper _mapper;

        public RoleService(IRoleRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<RoleResponseDto> CreateAsync(CreateRoleRequestDto dto)
        {
            // Use AutoMapper to map DTO to entity
            var entity = _mapper.Map<Role>(dto);

            var created = await _repo.CreateAsync(entity);
            return _mapper.Map<RoleResponseDto>(created);
        }

        public async Task<RoleResponseDto> GetByIdAsync(int roleId)
        {
            var role = await _repo.GetByIdAsync(roleId);
            return _mapper.Map<RoleResponseDto>(role);
        }

        public async Task<IEnumerable<RoleResponseDto>> GetAllAsync()
        {
            var roles = await _repo.GetAllAsync();
            return _mapper.Map<List<RoleResponseDto>>(roles);
        }

        public async Task<bool> DeleteAsync(int roleId)
        {
            return await _repo.DeleteAsync(roleId);
        }
    }
}

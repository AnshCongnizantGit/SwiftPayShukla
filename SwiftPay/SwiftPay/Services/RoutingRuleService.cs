using AutoMapper;
using SwiftPay.Domain.Remittance.Entities;
using SwiftPay.DTOs.RoutingDTO;
using SwiftPay.Repositories.Interfaces;
using SwiftPay.Services.Interfaces;

namespace SwiftPay.Services
{
    public class RoutingRuleService : IRoutingRuleService
    {
        private readonly IRoutingRuleRepository _repo;
        private readonly IMapper _mapper;

        public RoutingRuleService(IRoutingRuleRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<RoutingRuleResponseDto> CreateRuleAsync(CreateRoutingRuleDto dto)
        {
            var entity = _mapper.Map<RoutingRule>(dto);

            entity.RuleId = Guid.NewGuid().ToString();
            entity.CreatedDate = DateTimeOffset.UtcNow;
            entity.UpdateDate = DateTimeOffset.UtcNow;
            entity.IsDeleted = false;

            await _repo.AddAsync(entity);
            return _mapper.Map<RoutingRuleResponseDto>(entity);
        }

        public async Task<RoutingRuleResponseDto?> GetRuleByIdAsync(string id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return _mapper.Map<RoutingRuleResponseDto>(entity);
        }

        public async Task<IEnumerable<RoutingRuleResponseDto>> GetAllRulesAsync()
        {
            var entities = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<RoutingRuleResponseDto>>(entities);
        }

        public async Task<bool> UpdateRuleAsync(string id, CreateRoutingRuleDto dto)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return false;

            _mapper.Map(dto, existing);
            existing.UpdateDate = DateTimeOffset.UtcNow;

            await _repo.UpdateAsync(existing);
            return true;
        }

        public async Task<bool> DeleteRuleAsync(string id)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return false;
            await _repo.DeleteAsync(id);
            return true;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using SwiftPay.Constants.Enums;
using SwiftPay.DTOs.FXQuoteDTO;
using SwiftPay.FXModule.Api.Models;
using SwiftPay.Repositories.Interfaces;
using SwiftPay.Services.Interfaces;

namespace SwiftPay.Services
{
    public class FeeRuleService : IFeeRuleService
    {
        private readonly IFeeRuleRepository _repo;
        private readonly IMapper _mapper;

        public FeeRuleService(IFeeRuleRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<FeeRuleResponseDto> CreateFeeRuleAsync(CreateFeeRuleRequestDto request)
        {
            var newRule = _mapper.Map<FeeRule>(request);

            // Apply sensible defaults for optional dates so the non-nullable DB column is always satisfied.
            newRule.EffectiveFrom = request.EffectiveFrom?.ToUniversalTime() ?? DateTime.UtcNow.Date;
            newRule.EffectiveTo   = request.EffectiveTo?.ToUniversalTime()   ?? DateTime.UtcNow.Date.AddYears(10);
            newRule.Status        = RuleStatus.Active;

            var savedRule = await _repo.AddFeeRuleAsync(newRule);
            return _mapper.Map<FeeRuleResponseDto>(savedRule);
        }

        public async Task<IEnumerable<FeeRuleResponseDto>> GetActiveFeeRulesAsync()
        {
            var rules = await _repo.GetAllActiveFeeRulesAsync();
            return _mapper.Map<IEnumerable<FeeRuleResponseDto>>(rules);
        }

        public async Task<FeeRuleResponseDto> UpdateFeeRuleAsync(string id, UpdateFeeRuleRequestDto request)
        {
            var existingRule = await _repo.GetFeeRuleByIdAsync(id);
            if (existingRule == null) return null;

            _mapper.Map(request, existingRule);

            // Preserve existing dates when not explicitly changed.
            if (request.EffectiveFrom.HasValue)
                existingRule.EffectiveFrom = request.EffectiveFrom.Value.ToUniversalTime();
            if (request.EffectiveTo.HasValue)
                existingRule.EffectiveTo = request.EffectiveTo.Value.ToUniversalTime();

            existingRule.UpdateDate = DateTime.UtcNow;

            await _repo.UpdateFeeRuleAsync(existingRule);
            return _mapper.Map<FeeRuleResponseDto>(existingRule);
        }

        public async Task<bool> DeleteFeeRuleAsync(string id)
        {
            var existingRule = await _repo.GetFeeRuleByIdAsync(id);
            if (existingRule == null) return false;

            existingRule.IsDeleted  = true;
            existingRule.Status     = RuleStatus.Inactive;
            existingRule.UpdateDate = DateTime.UtcNow;

            await _repo.UpdateFeeRuleAsync(existingRule);
            return true;
        }
    }
}

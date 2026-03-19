using SwiftPay.DTOs.RoutingDTO;

namespace SwiftPay.Services.Interfaces
{
    public interface IRoutingRuleService
    {
        Task<RoutingRuleResponseDto> CreateRuleAsync(CreateRoutingRuleDto dto);
        Task<RoutingRuleResponseDto?> GetRuleByIdAsync(string id);
        Task<IEnumerable<RoutingRuleResponseDto>> GetAllRulesAsync();
        Task<bool> UpdateRuleAsync(string id, CreateRoutingRuleDto dto);
        Task<bool> DeleteRuleAsync(string id);
    }
}
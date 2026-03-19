using System;
using SwiftPay.Constants.Enums;
using System.ComponentModel.DataAnnotations;

namespace SwiftPay.DTOs.RoutingDTO
{
    public class CreateRoutingRuleDto
    {
        [Required]
        public string Corridor { get; set; } = string.Empty; // e.g., GBP-INR

        [Required]
        public PayoutModeStatus PayoutMode { get; set; }

        [Required]
        public string PartnerCode { get; set; } = string.Empty;

        [Required]
        public int Priority { get; set; }

        [Required]
        public RoutingRuleStatus Status { get; set; }
    }

    public class RoutingRuleResponseDto : CreateRoutingRuleDto
    {
        public string RuleId { get; set; } = string.Empty;
        public DateTimeOffset CreatedDate { get; set; }
    }
}
using System;
using SwiftPay.Constants.Enums; // Added reference to your Enums namespace

namespace SwiftPay.FXModule.Api.Models
{
    public class FeeRule
    {
        public string FeeRuleID { get; set; }
        public string Corridor { get; set; } 
        public PayoutMode PayoutMode { get; set; } // Updated from string
        public FeeType FeeType { get; set; } // Updated from string
        public decimal FeeValue { get; set; } 
        public decimal MinFee { get; set; } 
        public decimal MaxFee { get; set; } 
        public DateTime EffectiveFrom { get; set; } 
        public DateTime EffectiveTo { get; set; } 
        public RuleStatus Status { get; set; } // Updated from string

        // Audit & Soft Delete Fields
        public DateTime CreatedDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
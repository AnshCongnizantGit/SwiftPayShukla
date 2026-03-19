using System.ComponentModel.DataAnnotations;
using SwiftPay.Constants.Enums;

namespace SwiftPay.DTOs.UserCustomerDTO
{
    public class UpdateCustomerRiskRatingDto
    {
        [Required(ErrorMessage = "RiskRating is required.")]
        public CustomerRiskRating RiskRating { get; set; }
    }
}

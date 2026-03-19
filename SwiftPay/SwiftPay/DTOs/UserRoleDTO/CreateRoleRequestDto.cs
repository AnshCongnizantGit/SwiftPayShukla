using System.ComponentModel.DataAnnotations;
using SwiftPay.Constants.Enums;

namespace SwiftPay.DTOs.UserRoleDTO
{
    // DTO for creating a role
    public class CreateRoleRequestDto
    {
        [Required(ErrorMessage = "RoleType is required.")]
        public RoleType RoleType { get; set; }
    }
}

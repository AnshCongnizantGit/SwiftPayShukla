using System.ComponentModel.DataAnnotations;

namespace SwiftPay.DTOs.UserRoleDTO
{
    // DTO for assigning a role to a user
    public class CreateUserRoleRequestDto
    {
        [Required(ErrorMessage = "RoleId is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "RoleId must be a valid positive integer.")]
        public int RoleId { get; set; }
    }
}

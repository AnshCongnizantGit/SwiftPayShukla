using System;
using SwiftPay.Constants.Enums;

namespace SwiftPay.DTOs.UserRoleDTO
{
    // DTO for role response data
    public class RoleResponseDto
    {
        public int RoleId { get; set; }

        public RoleType RoleType { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}

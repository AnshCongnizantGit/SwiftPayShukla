using System;
using SwiftPay.Constants.Enums;

namespace SwiftPay.DTOs.UserRoleDTO
{
    // DTO for user role response data
    public class UserRoleResponseDto
    {
        public int UserRoleId { get; set; }

        public int UserId { get; set; }

        public int RoleId { get; set; }

        public RoleType RoleType { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}

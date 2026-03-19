using System.Collections.Generic;

namespace SwiftPay.DTOs.UserRoleDTO
{
    // DTO for role list response data
    public class RoleListResponseDto
    {
        public ICollection<RoleResponseDto> Roles { get; set; } = new List<RoleResponseDto>();

        public int TotalCount { get; set; }
    }
}

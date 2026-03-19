using System.Collections.Generic;
using System.Threading.Tasks;
using SwiftPay.DTOs.UserCustomerDTO;

namespace SwiftPay.Services.Interfaces
{
    public interface INotificationAlertService
    {
        Task<NotificationResponseDto> CreateAsync(CreateNotificationDto dto);
        Task<NotificationResponseDto> GetByIdAsync(int notificationId);
        Task<IEnumerable<NotificationResponseDto>> GetByUserIdAsync(int userId);
        Task<IEnumerable<NotificationResponseDto>> GetUnreadByUserIdAsync(int userId);
        Task<IEnumerable<NotificationResponseDto>> GetAllAsync();
        Task<NotificationResponseDto> MarkAsReadAsync(int notificationId);
        Task<IEnumerable<NotificationResponseDto>> MarkAllAsReadAsync(int userId);
        Task<bool> DeleteAsync(int notificationId);
    }
}

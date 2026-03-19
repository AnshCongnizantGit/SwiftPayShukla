using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SwiftPay.Services.Interfaces;
using SwiftPay.DTOs.UserCustomerDTO;

namespace SwiftPay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationAlertService _service;

        public NotificationsController(INotificationAlertService service)
        {
            _service = service;
        }

        /// <summary>
        /// Create a new notification
        /// </summary>
        /// <param name="dto">Notification creation data</param>
        /// <returns>Created notification DTO</returns>
        /// <response code="200">Notification created successfully</response>
        /// <response code="400">Invalid request data</response>
        /// <response code="500">Server error</response>
        [HttpPost]
        [ProducesResponseType(typeof(NotificationResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CreateNotificationDto dto)
        {
            try
            {
                var created = await _service.CreateAsync(dto);
                return Ok(new { message = "Notification created successfully.", data = created });
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the notification.", error = ex.Message });
            }
        }

        /// <summary>
        /// Get notification by ID
        /// </summary>
        /// <param name="notificationId">Notification ID</param>
        /// <returns>Notification DTO</returns>
        /// <response code="200">Notification found</response>
        /// <response code="404">Notification not found</response>
        /// <response code="500">Server error</response>
        [HttpGet("{notificationId}")]
        [ProducesResponseType(typeof(NotificationResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int notificationId)
        {
            try
            {
                var notification = await _service.GetByIdAsync(notificationId);
                if (notification == null)
                    return NotFound(new { message = $"Notification with ID {notificationId} not found." });

                return Ok(new { message = "Notification retrieved successfully.", data = notification });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving the notification.", error = ex.Message });
            }
        }

        /// <summary>
        /// Get all notifications for a user
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <returns>List of notification DTOs for the user</returns>
        /// <response code="200">Notifications retrieved successfully</response>
        /// <response code="500">Server error</response>
        [HttpGet("user/{userId}")]
        [ProducesResponseType(typeof(IEnumerable<NotificationResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByUserId(int userId)
        {
            try
            {
                var notifications = await _service.GetByUserIdAsync(userId);
                return Ok(new { message = "Notifications retrieved successfully.", data = notifications });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving notifications.", error = ex.Message });
            }
        }

        /// <summary>
        /// Get unread notifications for a user
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <returns>List of unread notification DTOs</returns>
        /// <response code="200">Unread notifications retrieved successfully</response>
        /// <response code="500">Server error</response>
        [HttpGet("user/{userId}/unread")]
        [ProducesResponseType(typeof(IEnumerable<NotificationResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUnreadByUserId(int userId)
        {
            try
            {
                var notifications = await _service.GetUnreadByUserIdAsync(userId);
                return Ok(new { message = "Unread notifications retrieved successfully.", data = notifications });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving unread notifications.", error = ex.Message });
            }
        }

        /// <summary>
        /// Get all notifications
        /// </summary>
        /// <returns>List of all notification DTOs</returns>
        /// <response code="200">Notifications retrieved successfully</response>
        /// <response code="500">Server error</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<NotificationResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var notifications = await _service.GetAllAsync();
                return Ok(new { message = "Notifications retrieved successfully.", data = notifications });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving notifications.", error = ex.Message });
            }
        }

        /// <summary>
        /// Mark notification as read
        /// </summary>
        /// <param name="notificationId">Notification ID</param>
        /// <returns>Updated notification DTO</returns>
        /// <response code="200">Notification marked as read</response>
        /// <response code="404">Notification not found</response>
        /// <response code="500">Server error</response>
        [HttpPut("{notificationId}/read")]
        [ProducesResponseType(typeof(NotificationResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> MarkAsRead(int notificationId)
        {
            try
            {
                var notification = await _service.MarkAsReadAsync(notificationId);
                return Ok(new { message = "Notification marked as read.", data = notification });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while marking the notification as read.", error = ex.Message });
            }
        }

        /// <summary>
        /// Mark all notifications as read for a user
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <returns>List of marked notification DTOs</returns>
        /// <response code="200">Notifications marked as read</response>
        /// <response code="500">Server error</response>
        [HttpPut("user/{userId}/read-all")]
        [ProducesResponseType(typeof(IEnumerable<NotificationResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> MarkAllAsRead(int userId)
        {
            try
            {
                var notifications = await _service.MarkAllAsReadAsync(userId);
                return Ok(new { message = "All notifications marked as read.", data = notifications });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while marking notifications as read.", error = ex.Message });
            }
        }

        /// <summary>
        /// Delete notification
        /// </summary>
        /// <param name="notificationId">Notification ID</param>
        /// <returns>Deletion result</returns>
        /// <response code="200">Notification deleted successfully</response>
        /// <response code="404">Notification not found</response>
        /// <response code="500">Server error</response>
        [HttpDelete("{notificationId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int notificationId)
        {
            try
            {
                var deleted = await _service.DeleteAsync(notificationId);
                if (!deleted)
                    return NotFound(new { message = $"Notification with ID {notificationId} does not exist in the system and cannot be deleted." });

                return Ok(new { message = "Notification deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the notification. Please try again later.", error = ex.Message });
            }
        }
    }
}

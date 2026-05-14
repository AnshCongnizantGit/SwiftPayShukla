using Microsoft.AspNetCore.Mvc;
using Moq;
using SwiftPay.Constants.Enums;
using SwiftPay.Controllers;
using SwiftPay.DTOs.UserCustomerDTO;
using SwiftPay.Services.Interfaces;

namespace Swiftpay.Tests.Controllers
{
    [TestFixture]
    public class NotificationsControllerTests
    {
        private Mock<INotificationAlertService> _mockNotificationService;
        private NotificationsController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockNotificationService = new Mock<INotificationAlertService>();
            _controller = new NotificationsController(_mockNotificationService.Object);
        }

        // Operation tested: Create (CREATE)
        // Verifies that a valid notification creation request returns HTTP 200.
        [Test]
        public async Task Create_ReturnsOk_WhenNotificationCreatedSuccessfully()
        {
            // Arrange
            var dto = new CreateNotificationDto
            {
                UserID   = 1,
                RemitID  = 50,
                Message  = "Your remittance has been processed.",
                Category = NotificationCategory.Routing
            };
            var response = new NotificationResponseDto
            {
                NotificationID = 1,
                UserID         = 1,
                Message        = dto.Message
            };
            _mockNotificationService.Setup(s => s.CreateAsync(It.IsAny<CreateNotificationDto>()))
                                    .ReturnsAsync(response);

            // Act
            var result = await _controller.Create(dto);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult!.StatusCode, Is.EqualTo(200));
        }
    }
}

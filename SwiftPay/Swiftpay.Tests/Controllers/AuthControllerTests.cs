using Microsoft.AspNetCore.Mvc;
using Moq;
using SwiftPay.Controllers;
using SwiftPay.DTOs.UserCustomerDTO;
using SwiftPay.Services.Interfaces;

namespace Swiftpay.Tests.Controllers
{
    [TestFixture]
    public class AuthControllerTests
    {
        private Mock<IAuthService> _mockAuthService;
        private AuthController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockAuthService = new Mock<IAuthService>();
            _controller = new AuthController(_mockAuthService.Object);
        }

        // Operation tested: Register (CREATE)
        // Verifies that a valid registration request returns HTTP 200 with the new user data.
        [Test]
        public async Task Register_ReturnsOk_WhenRegistrationSucceeds()
        {
            // Arrange
            var dto = new RegisterUserDto
            {
                Name = "Test User",
                Email = "test@swiftpay.com",
                Phone = "1234567890",
                Password = "SecurePass123"
            };
            var authResponse = new AuthResponseDto
            {
                UserId = 1,
                Name = dto.Name,
                Email = dto.Email
            };
            _mockAuthService.Setup(s => s.RegisterAsync(It.IsAny<RegisterUserDto>()))
                            .ReturnsAsync(authResponse);

            // Act
            var result = await _controller.Register(dto);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult!.StatusCode, Is.EqualTo(200));
        }
    }
}

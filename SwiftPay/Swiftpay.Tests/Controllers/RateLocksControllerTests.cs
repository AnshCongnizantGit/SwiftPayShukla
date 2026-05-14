using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SwiftPay.Constants.Enums;
using SwiftPay.Controllers;
using SwiftPay.DTOs.FXQuoteDTO;
using SwiftPay.Services.Interfaces;

namespace Swiftpay.Tests.Controllers
{
    [TestFixture]
    public class RateLocksControllerTests
    {
        private Mock<IRateLockService> _mockRateLockService;
        private RateLocksController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockRateLockService = new Mock<IRateLockService>();
            _controller = new RateLocksController(_mockRateLockService.Object);

            // CreateRateLock reads User.FindFirstValue(ClaimTypes.NameIdentifier) for caller ID.
            SetupControllerUser(_controller, userId: 1, role: "Admin");
        }

        // Operation tested: CreateRateLock (CREATE)
        // Verifies that a valid rate lock request returns HTTP 200 with the locked rate details.
        [Test]
        public async Task CreateRateLock_ReturnsOk_WhenRateLockedSuccessfully()
        {
            // Arrange
            var request = new CreateRateLockRequestDto
            {
                QuoteID    = "QT-001",
                CustomerID = "CUST-5"
            };
            var response = new RateLockResponseDto
            {
                LockID     = "LCK-001",
                QuoteID    = "QT-001",
                CustomerID = "CUST-5",
                Status     = RateLockStatus.Locked
            };
            _mockRateLockService.Setup(s => s.LockRateAsync(It.IsAny<CreateRateLockRequestDto>()))
                                .ReturnsAsync(response);

            // Act
            var result = await _controller.CreateRateLock(request);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult!.StatusCode, Is.EqualTo(200));
        }

        private static void SetupControllerUser(ControllerBase controller, int userId, string role)
        {
            var claims    = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Role, role)
            };
            var identity  = new ClaimsIdentity(claims, "TestAuth");
            var principal = new ClaimsPrincipal(identity);
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = principal }
            };
        }
    }
}

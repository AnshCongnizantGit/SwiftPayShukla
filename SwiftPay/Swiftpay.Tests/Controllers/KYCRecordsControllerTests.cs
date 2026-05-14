using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SwiftPay.Constants.Enums;
using SwiftPay.Controllers;
using SwiftPay.DTOs.UserCustomerDTO;
using SwiftPay.Services.Interfaces;

namespace Swiftpay.Tests.Controllers
{
    [TestFixture]
    public class KYCRecordsControllerTests
    {
        private Mock<IKYCRecordService> _mockKycRecordService;
        private KYCRecordsController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockKycRecordService = new Mock<IKYCRecordService>();
            _controller = new KYCRecordsController(_mockKycRecordService.Object);

            // Create reads User.FindFirst(ClaimTypes.NameIdentifier) to determine ownership.
            SetupControllerUser(_controller, userId: 1, role: "Admin");
        }

        // Operation tested: Create (CREATE)
        // Verifies that submitting a valid KYC record creation request returns HTTP 200.
        [Test]
        public async Task Create_ReturnsOk_WhenKYCRecordCreatedSuccessfully()
        {
            // Arrange
            var dto = new CreateKYCRecordDto
            {
                UserID   = 1,
                KYCLevel = KYCLevel.Min
            };
            var response = new KYCRecordResponseDto
            {
                KYCID    = 1,
                UserID   = 1,
                KYCLevel = KYCLevel.Min
            };
            _mockKycRecordService.Setup(s => s.CreateAsync(It.IsAny<CreateKYCRecordDto>()))
                                 .ReturnsAsync(response);

            // Act
            var result = await _controller.Create(dto);

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

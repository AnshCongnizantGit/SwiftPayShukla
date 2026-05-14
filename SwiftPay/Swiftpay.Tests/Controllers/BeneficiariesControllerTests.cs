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
    public class BeneficiariesControllerTests
    {
        private Mock<IBeneficiaryService> _mockBeneficiaryService;
        private Mock<ICustomerService> _mockCustomerService;
        private BeneficiariesController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockBeneficiaryService = new Mock<IBeneficiaryService>();
            _mockCustomerService    = new Mock<ICustomerService>();
            _controller = new BeneficiariesController(
                _mockBeneficiaryService.Object,
                _mockCustomerService.Object);

            // Create provides the role-based claim; Admin bypasses the customer-link check.
            SetupControllerUser(_controller, userId: 1, role: "Admin");
        }

        // Operation tested: Create (CREATE)
        // Verifies that an Admin user creating a beneficiary returns HTTP 200.
        [Test]
        public async Task Create_ReturnsOk_WhenBeneficiaryCreatedSuccessfully()
        {
            // Arrange
            var dto = new CreateBeneficiaryDto
            {
                CustomerID        = 5,
                Name              = "John Doe",
                Country           = "IN",
                PayoutMode        = PayoutMode.Account,
                AccountOrWalletNo = "1234567890"
            };
            var response = new BeneficiaryResponseDto
            {
                BeneficiaryID     = 1,
                CustomerID        = 5,
                Name              = "John Doe",
                Country           = "IN",
                AccountOrWalletNo = "1234567890"
            };
            _mockBeneficiaryService.Setup(s => s.CreateAsync(It.IsAny<CreateBeneficiaryDto>()))
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

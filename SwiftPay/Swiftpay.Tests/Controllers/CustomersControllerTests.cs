using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SwiftPay.Controllers;
using SwiftPay.DTOs.UserCustomerDTO;
using SwiftPay.Services.Interfaces;

namespace Swiftpay.Tests.Controllers
{
    [TestFixture]
    public class CustomersControllerTests
    {
        private Mock<ICustomerService>    _mockCustomerService;
        private Mock<IBeneficiaryService> _mockBeneficiaryService;
        private Mock<IUserService>        _mockUserService;
        private CustomersController       _controller;

        [SetUp]
        public void SetUp()
        {
            _mockCustomerService    = new Mock<ICustomerService>();
            _mockBeneficiaryService = new Mock<IBeneficiaryService>();
            _mockUserService        = new Mock<IUserService>();
            _controller = new CustomersController(
                _mockCustomerService.Object,
                _mockBeneficiaryService.Object,
                _mockUserService.Object);

            // Admin bypasses customer-profile ownership logic inside Create.
            SetupControllerUser(_controller, userId: 1, role: "Admin");
        }

        // Operation tested: Create (CREATE)
        // Verifies that an Admin user creating a customer profile returns HTTP 200.
        [Test]
        public async Task Create_ReturnsOk_WhenCustomerCreatedSuccessfully()
        {
            // Arrange
            var dto = new CreateCustomerDto
            {
                UserID      = 1,
                Nationality = "IN",
                AddressJSON = "{\"city\":\"Mumbai\"}"
            };
            var response = new CustomerResponseDto
            {
                CustomerID  = 10,
                UserID      = 1,
                Nationality = "IN"
            };
            _mockCustomerService.Setup(s => s.CreateAsync(It.IsAny<CreateCustomerDto>()))
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

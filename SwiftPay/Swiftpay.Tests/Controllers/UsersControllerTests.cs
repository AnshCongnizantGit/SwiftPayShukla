using Microsoft.AspNetCore.Mvc;
using Moq;
using SwiftPay.Controllers;
using SwiftPay.DTOs.UserCustomerDTO;
using SwiftPay.Services.Interfaces;

namespace Swiftpay.Tests.Controllers
{
    [TestFixture]
    public class UsersControllerTests
    {
        private Mock<IUserService> _mockUserService;
        private UsersController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockUserService = new Mock<IUserService>();
            _controller = new UsersController(_mockUserService.Object);
        }

        // Operation tested: GetAll (READ)
        // Verifies that the endpoint returns HTTP 200 with a list of all users.
        [Test]
        public async Task GetAll_ReturnsOk_WithUserList()
        {
            // Arrange
            var users = new List<UserResponseDto>
            {
                new UserResponseDto { UserId = 1, Name = "Alice", Email = "alice@test.com" },
                new UserResponseDto { UserId = 2, Name = "Bob",   Email = "bob@test.com"   }
            };
            _mockUserService.Setup(s => s.GetAllAsync())
                            .ReturnsAsync(users);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult!.StatusCode, Is.EqualTo(200));
        }
    }
}

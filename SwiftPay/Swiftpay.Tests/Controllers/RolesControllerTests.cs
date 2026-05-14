using Microsoft.AspNetCore.Mvc;
using Moq;
using SwiftPay.Constants.Enums;
using SwiftPay.Controllers;
using SwiftPay.DTOs.UserRoleDTO;
using SwiftPay.Services.Interfaces;

namespace Swiftpay.Tests.Controllers
{
    [TestFixture]
    public class RolesControllerTests
    {
        private Mock<IRoleService>     _mockRoleService;
        private Mock<IUserRoleService> _mockUserRoleService;
        private Mock<IUserService>     _mockUserService;
        private RolesController        _controller;

        [SetUp]
        public void SetUp()
        {
            _mockRoleService     = new Mock<IRoleService>();
            _mockUserRoleService = new Mock<IUserRoleService>();
            _mockUserService     = new Mock<IUserService>();
            _controller = new RolesController(
                _mockRoleService.Object,
                _mockUserRoleService.Object,
                _mockUserService.Object);
        }

        // Operation tested: GetById (READ)
        // Verifies that fetching an existing role by ID returns HTTP 200 with the role.
        [Test]
        public async Task GetById_ReturnsOk_WhenRoleExists()
        {
            // Arrange
            int roleId   = 1;
            var response = new RoleResponseDto { RoleId = roleId, RoleType = RoleType.Admin };
            _mockRoleService.Setup(s => s.GetByIdAsync(roleId))
                            .ReturnsAsync(response);

            // Act
            var result = await _controller.GetById(roleId);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult!.StatusCode, Is.EqualTo(200));
        }
    }
}

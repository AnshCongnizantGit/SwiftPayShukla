using Microsoft.AspNetCore.Mvc;
using Moq;
using SwiftPay.Controllers;
using SwiftPay.DTOs.UserCustomerDTO;
using SwiftPay.Services.Interfaces;

namespace Swiftpay.Tests.Controllers
{
    [TestFixture]
    public class AuditLogsControllerTests
    {
        private Mock<IAuditLogService> _mockAuditLogService;
        private AuditLogsController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockAuditLogService = new Mock<IAuditLogService>();
            _controller = new AuditLogsController(_mockAuditLogService.Object);
        }

        // Operation tested: GetAuditLogsFiltered (READ)
        // Verifies that a filtered audit log query returns HTTP 200 with a paginated result.
        [Test]
        public async Task GetAuditLogsFiltered_ReturnsOk_WithAuditLogList()
        {
            // Arrange
            var filter = new AuditLogFilterDto { PageNumber = 1, PageSize = 10 };
            var pagedResult = new AuditLogListDto
            {
                TotalCount  = 2,
                PageNumber  = 1,
                PageSize    = 10,
                AuditLogs   = new List<GetAuditLogDto>
                {
                    new GetAuditLogDto { AuditID = 1, Action = "Login",  Resource = "Auth" },
                    new GetAuditLogDto { AuditID = 2, Action = "Create", Resource = "User" }
                }
            };
            _mockAuditLogService
                .Setup(s => s.GetFilteredAsync(
                    filter.UserId,
                    filter.Resource,
                    filter.StartDate,
                    filter.EndDate,
                    filter.PageNumber,
                    filter.PageSize))
                .ReturnsAsync(pagedResult);

            // Act
            var result = await _controller.GetAuditLogsFiltered(filter);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult!.StatusCode, Is.EqualTo(200));
        }
    }
}

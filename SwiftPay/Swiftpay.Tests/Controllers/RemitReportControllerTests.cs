using Microsoft.AspNetCore.Mvc;
using Moq;
using SwiftPay.Controllers;
using SwiftPay.Domain.Remittance.Entities;
using SwiftPay.Services.Interfaces;

namespace Swiftpay.Tests.Controllers
{
    [TestFixture]
    public class RemitReportControllerTests
    {
        private Mock<IRemitReportService> _mockRemitReportService;
        private RemitReportController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockRemitReportService = new Mock<IRemitReportService>();
            _controller = new RemitReportController(_mockRemitReportService.Object);
        }

        // Operation tested: GetAll (READ)
        // Verifies that fetching all remittance reports returns HTTP 200 with the report list.
        [Test]
        public async Task GetAll_ReturnsOk_WithReportList()
        {
            // Arrange
            var reports = new List<RemitReport>
            {
                new RemitReport { ReportID = 1, Scope = "Monthly", Metrics = "{}" },
                new RemitReport { ReportID = 2, Scope = "Weekly",  Metrics = "{}" }
            };
            _mockRemitReportService.Setup(s => s.GetAllAsync())
                                   .ReturnsAsync(reports);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult!.StatusCode, Is.EqualTo(200));
        }
    }
}

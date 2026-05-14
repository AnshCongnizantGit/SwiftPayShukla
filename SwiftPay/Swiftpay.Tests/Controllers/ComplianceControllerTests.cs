using Microsoft.AspNetCore.Mvc;
using Moq;
using SwiftPay.Controllers;
using SwiftPay.Domain.Remittance.Entities;
using SwiftPay.DTOs.ComplianceDTO;
using SwiftPay.Services.Interfaces;

namespace Swiftpay.Tests.Controllers
{
    [TestFixture]
    public class ComplianceControllerTests
    {
        private Mock<IComplianceCheckService> _mockComplianceService;
        private ComplianceController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockComplianceService = new Mock<IComplianceCheckService>();
            _controller = new ComplianceController(_mockComplianceService.Object);
        }

        // Operation tested: Create (CREATE)
        // Verifies that a valid compliance check creation request returns HTTP 200.
        [Test]
        public async Task Create_ReturnsOk_WhenComplianceCheckCreatedSuccessfully()
        {
            // Arrange
            var dto = new CreateComplianceCheckDto
            {
                RemitId   = 20,
                CheckType = "Sanctions",
                Result    = "Clear",
                Severity  = "Low",
                Remarks   = "No issues"
            };
            var created = new ComplianceCheck { CheckId = "CC-001" };
            _mockComplianceService.Setup(s => s.CreateAsync(It.IsAny<CreateComplianceCheckDto>()))
                                  .ReturnsAsync(created);

            // Act
            var result = await _controller.Create(dto);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult!.StatusCode, Is.EqualTo(200));
        }
    }
}

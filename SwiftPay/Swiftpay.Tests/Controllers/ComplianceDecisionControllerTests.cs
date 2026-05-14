using Microsoft.AspNetCore.Mvc;
using Moq;
using SwiftPay.Controllers;
using SwiftPay.Domain.Remittance.Entities;
using SwiftPay.Services.Interfaces;

namespace Swiftpay.Tests.Controllers
{
    [TestFixture]
    public class ComplianceDecisionControllerTests
    {
        private Mock<IComplianceDecisionService> _mockDecisionService;
        private ComplianceDecisionController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockDecisionService = new Mock<IComplianceDecisionService>();
            _controller = new ComplianceDecisionController(_mockDecisionService.Object);
        }

        // Operation tested: GetByRemitId (READ)
        // Verifies that fetching compliance decisions for a remit ID returns HTTP 200.
        [Test]
        public async Task GetByRemitId_ReturnsOk_WithDecisionList()
        {
            // Arrange
            var remitId   = "RMT-001";
            var decisions = new List<ComplianceDecision>
            {
                new ComplianceDecision { DecisionId = "DEC-1", RemitId = remitId },
                new ComplianceDecision { DecisionId = "DEC-2", RemitId = remitId }
            };
            _mockDecisionService
                .Setup(s => s.GetDecisionsByRemittanceAsync(remitId))
                .ReturnsAsync(decisions);

            // Act
            var result = await _controller.GetByRemitId(remitId);

            // Assert – GetByRemitId returns ActionResult<IEnumerable<ComplianceDecision>>
            // so the actual IActionResult lives in .Result.
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult!.StatusCode, Is.EqualTo(200));
        }
    }
}

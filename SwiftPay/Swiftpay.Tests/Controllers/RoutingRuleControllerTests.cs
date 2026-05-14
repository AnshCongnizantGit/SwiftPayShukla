using Microsoft.AspNetCore.Mvc;
using Moq;
using SwiftPay.Controllers;
using SwiftPay.DTOs.RoutingDTO;
using SwiftPay.Services.Interfaces;

namespace Swiftpay.Tests.Controllers
{
    [TestFixture]
    public class RoutingRuleControllerTests
    {
        private Mock<IRoutingRuleService> _mockRoutingRuleService;
        private RoutingRuleController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockRoutingRuleService = new Mock<IRoutingRuleService>();
            _controller = new RoutingRuleController(_mockRoutingRuleService.Object);
        }

        // Operation tested: GetAll (READ)
        // Verifies that fetching all routing rules returns HTTP 200 with the rule list.
        [Test]
        public async Task GetAll_ReturnsOk_WithRoutingRuleList()
        {
            // Arrange
            var rules = new List<RoutingRuleResponseDto>
            {
                new RoutingRuleResponseDto
                {
                    RuleId      = "RR-001",
                    Corridor    = "GBP-INR",
                    PartnerCode = "PART-A",
                    Priority    = 1
                }
            };
            _mockRoutingRuleService.Setup(s => s.GetAllRulesAsync())
                                   .ReturnsAsync(rules);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult!.StatusCode, Is.EqualTo(200));
        }
    }
}

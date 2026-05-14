using Microsoft.AspNetCore.Mvc;
using Moq;
using SwiftPay.Constants.Enums;
using SwiftPay.Controllers;
using SwiftPay.DTOs.FXQuoteDTO;
using SwiftPay.Services.Interfaces;

namespace Swiftpay.Tests.Controllers
{
    [TestFixture]
    public class FeeRulesControllerTests
    {
        private Mock<IFeeRuleService> _mockFeeRuleService;
        private FeeRulesController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockFeeRuleService = new Mock<IFeeRuleService>();
            _controller = new FeeRulesController(_mockFeeRuleService.Object);
        }

        // Operation tested: GetActiveFeeRules (READ)
        // Verifies that fetching all active fee rules returns HTTP 200 with the rule list.
        [Test]
        public async Task GetActiveFeeRules_ReturnsOk_WithFeeRuleList()
        {
            // Arrange
            var rules = new List<FeeRuleResponseDto>
            {
                new FeeRuleResponseDto
                {
                    FeeRuleID  = "FR-001",
                    Corridor   = "GBP-INR",
                    FeeType    = FeeType.Flat,
                    FeeValue   = 5m,
                    Status     = RuleStatus.Active
                }
            };
            _mockFeeRuleService.Setup(s => s.GetActiveFeeRulesAsync())
                               .ReturnsAsync(rules);

            // Act
            var result = await _controller.GetActiveFeeRules();

            // Assert
            var okResult = result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult!.StatusCode, Is.EqualTo(200));
        }
    }
}

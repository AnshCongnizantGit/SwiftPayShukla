using Microsoft.AspNetCore.Mvc;
using Moq;
using SwiftPay.Controllers;
using SwiftPay.DTOs.PayoutDTO;
using SwiftPay.Services.Interfaces;

namespace Swiftpay.Tests.Controllers
{
    [TestFixture]
    public class PayoutInstructionControllerTests
    {
        private Mock<IPayoutInstructionService> _mockPayoutService;
        private PayoutInstructionController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockPayoutService = new Mock<IPayoutInstructionService>();
            _controller = new PayoutInstructionController(_mockPayoutService.Object);
        }

        // Operation tested: GetAll (READ)
        // Verifies that fetching all payout instructions returns HTTP 200 with the instruction list.
        [Test]
        public async Task GetAll_ReturnsOk_WithPayoutInstructionList()
        {
            // Arrange
            var instructions = new List<PayoutInstructionResponseDto>
            {
                new PayoutInstructionResponseDto
                {
                    InstructionId = "PI-001",
                    RemitId       = "RMT-10",
                    PartnerCode   = "PARTNER_A"
                }
            };
            _mockPayoutService.Setup(s => s.GetAllAsync())
                              .ReturnsAsync(instructions);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult!.StatusCode, Is.EqualTo(200));
        }
    }
}

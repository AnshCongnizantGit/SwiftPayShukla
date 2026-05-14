using Microsoft.AspNetCore.Mvc;
using Moq;
using SwiftPay.Controllers;
using SwiftPay.DTOs.SettlementDTO;
using SwiftPay.Models;
using SwiftPay.Services.Interfaces;

namespace Swiftpay.Tests.Controllers
{
    [TestFixture]
    public class SettlementBatchControllerTests
    {
        private Mock<ISettlementBatchService> _mockSettlementBatchService;
        private SettlementBatchController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockSettlementBatchService = new Mock<ISettlementBatchService>();
            _controller = new SettlementBatchController(_mockSettlementBatchService.Object);
        }

        // Operation tested: GenerateBatch (CREATE)
        // Verifies that generating a settlement batch returns HTTP 201 Created with the batch.
        // The controller uses CreatedAtAction (201) rather than Ok (200) for this endpoint.
        [Test]
        public async Task GenerateBatch_Returns201Created_WhenBatchGeneratedSuccessfully()
        {
            // Arrange
            var dto = new GenerateBatchDto
            {
                Corridor    = "GBP-INR",
                PeriodStart = DateTimeOffset.UtcNow.AddDays(-7),
                PeriodEnd   = DateTimeOffset.UtcNow
            };
            var batch = new SettlementBatch { BatchID = 1, Corridor = "GBP-INR" };
            _mockSettlementBatchService.Setup(s => s.GenerateBatchAsync(It.IsAny<GenerateBatchDto>()))
                                       .ReturnsAsync(batch);

            // Act
            var result = await _controller.GenerateBatch(dto);

            // Assert – GenerateBatch returns ActionResult<SettlementBatch>;
            // the controller calls CreatedAtAction, so .Result is a CreatedAtActionResult.
            var createdResult = result.Result as CreatedAtActionResult;
            Assert.That(createdResult, Is.Not.Null);
            Assert.That(createdResult!.StatusCode, Is.EqualTo(201));
        }
    }
}

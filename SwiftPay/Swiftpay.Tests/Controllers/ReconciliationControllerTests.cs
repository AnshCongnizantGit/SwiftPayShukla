using Microsoft.AspNetCore.Mvc;
using Moq;
using SwiftPay.Controllers;
using SwiftPay.Models;
using SwiftPay.Services.Interfaces;

namespace Swiftpay.Tests.Controllers
{
    [TestFixture]
    public class ReconciliationControllerTests
    {
        private Mock<IReconciliationService> _mockReconciliationService;
        private ReconciliationController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockReconciliationService = new Mock<IReconciliationService>();
            _controller = new ReconciliationController(_mockReconciliationService.Object);
        }

        // Operation tested: ReconcileBatch (READ/PROCESS)
        // Verifies that reconciling a batch returns HTTP 200 with the reconciliation records.
        [Test]
        public async Task ReconcileBatch_ReturnsOk_WithReconciliationRecords()
        {
            // Arrange
            int batchId = 3;
            var records = new List<ReconciliationRecord>
            {
                new ReconciliationRecord { ReconID = 1, ReferenceID = "RMT-001" },
                new ReconciliationRecord { ReconID = 2, ReferenceID = "RMT-002" }
            };
            _mockReconciliationService.Setup(s => s.ReconcileBatchAsync(batchId))
                                      .ReturnsAsync(records);

            // Act
            var result = await _controller.ReconcileBatch(batchId);

            // Assert – ReconcileBatch returns ActionResult<IEnumerable<ReconciliationRecord>>
            // so the actual IActionResult lives in .Result.
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult!.StatusCode, Is.EqualTo(200));
        }
    }
}

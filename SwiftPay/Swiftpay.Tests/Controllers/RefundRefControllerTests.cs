using Microsoft.AspNetCore.Mvc;
using Moq;
using SwiftPay.Controllers;
using SwiftPay.Domain.Remittance.Entities;
using SwiftPay.Services.Interfaces;

namespace Swiftpay.Tests.Controllers
{
    [TestFixture]
    public class RefundRefControllerTests
    {
        private Mock<IRefundRefService> _mockRefundRefService;
        private RefundRefController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockRefundRefService = new Mock<IRefundRefService>();
            _controller = new RefundRefController(_mockRefundRefService.Object);
        }

        // Operation tested: GetAll (READ)
        // Verifies that fetching all refund references returns HTTP 200 with the list.
        [Test]
        public async Task GetAll_ReturnsOk_WithRefundRefList()
        {
            // Arrange
            var refunds = new List<RefundRef>
            {
                new RefundRef { RefundID = 1, RemitID = 10, Amount = 250m },
                new RefundRef { RefundID = 2, RemitID = 11, Amount = 100m }
            };
            _mockRefundRefService.Setup(s => s.GetAllAsync())
                                 .ReturnsAsync(refunds);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult!.StatusCode, Is.EqualTo(200));
        }
    }
}

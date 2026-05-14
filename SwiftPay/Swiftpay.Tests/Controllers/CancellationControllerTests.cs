using Microsoft.AspNetCore.Mvc;
using Moq;
using SwiftPay.Controllers;
using SwiftPay.Domain.Remittance.Entities;
using SwiftPay.DTOs.CancellationDTO;
using SwiftPay.Services.Interfaces;

namespace Swiftpay.Tests.Controllers
{
    [TestFixture]
    public class CancellationControllerTests
    {
        private Mock<ICancellationService> _mockCancellationService;
        private CancellationController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockCancellationService = new Mock<ICancellationService>();
            _controller = new CancellationController(_mockCancellationService.Object);
        }

        // Operation tested: Create (CREATE)
        // Verifies that submitting a valid cancellation request returns HTTP 200.
        [Test]
        public async Task Create_ReturnsOk_WhenCancellationCreatedSuccessfully()
        {
            // Arrange
            var dto = new CreateCancellationDto
            {
                RemitId = 15,
                Reason  = "Customer changed mind"
            };
            var created = new Cancellation { CancellationID = 1, RemitID = 15 };
            _mockCancellationService.Setup(s => s.CreateAsync(It.IsAny<CreateCancellationDto>()))
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

using Microsoft.AspNetCore.Mvc;
using Moq;
using SwiftPay.Controllers;
using SwiftPay.Domain.Remittance.Entities;
using SwiftPay.DTOs.AmendmentDTO;
using SwiftPay.Services.Interfaces;

namespace Swiftpay.Tests.Controllers
{
    [TestFixture]
    public class AmendmentControllerTests
    {
        private Mock<IAmendmentService> _mockAmendmentService;
        private AmendmentController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockAmendmentService = new Mock<IAmendmentService>();
            _controller = new AmendmentController(_mockAmendmentService.Object);
        }

        // Operation tested: Create (CREATE)
        // Verifies that submitting a valid amendment returns HTTP 200 with the created amendment.
        [Test]
        public async Task Create_ReturnsOk_WhenAmendmentCreatedSuccessfully()
        {
            // Arrange
            var dto = new CreateAmendmentDto
            {
                RemitId      = 10,
                FieldChanged = "BeneficiaryAccount",
                OldValue     = "OLD123",
                NewValue     = "NEW456",
                RequestedBy  = 1
            };
            var created = new Amendment { AmendmentID = 1, RemitID = 10 };
            _mockAmendmentService.Setup(s => s.CreateAsync(It.IsAny<CreateAmendmentDto>()))
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

using Microsoft.AspNetCore.Mvc;
using Moq;
using SwiftPay.Constants.Enums;
using SwiftPay.Controllers;
using SwiftPay.DTOs.RemittanceDTO;
using SwiftPay.Services.Interfaces;

namespace Swiftpay.Tests.Controllers
{
    [TestFixture]
    public class DocumentControllerTests
    {
        private Mock<IDocumentService> _mockDocumentService;
        private DocumentController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockDocumentService = new Mock<IDocumentService>();
            _controller = new DocumentController(_mockDocumentService.Object);
        }

        // Operation tested: Create (CREATE)
        // Verifies that a valid document creation request returns HTTP 200 with the created document.
        [Test]
        public async Task Create_ReturnsOk_WhenDocumentCreatedSuccessfully()
        {
            // Arrange
            var dto = new CreateDocumentDto
            {
                RemitId = 10,
                DocType = DocumentType.IDProof,
                FileURI = "storage/kyc/doc1.pdf"
            };
            var response = new DocumentResponseDto
            {
                DocumentId = 1,
                RemitId    = 10,
                DocType    = "ProofOfFunds"
            };
            _mockDocumentService.Setup(s => s.CreateAsync(It.IsAny<CreateDocumentDto>()))
                                .ReturnsAsync(response);

            // Act
            var result = await _controller.Create(dto);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult!.StatusCode, Is.EqualTo(200));
        }
    }
}

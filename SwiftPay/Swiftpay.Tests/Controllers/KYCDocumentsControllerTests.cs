using System.Security.Claims;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SwiftPay.Controllers;
using SwiftPay.Domain.Remittance.Entities;
using SwiftPay.DTOs.KycAuditRecordDTO;
using SwiftPay.Repositories.Interfaces;
using SwiftPay.Services.Interfaces;

namespace Swiftpay.Tests.Controllers
{
    [TestFixture]
    public class KYCDocumentsControllerTests
    {
        private Mock<IKYCDocumentService>  _mockKycDocService;
        private Mock<IKYCRecordRepository> _mockKycRepo;
        private Mock<IWebHostEnvironment>  _mockEnv;
        private KYCDocumentsController     _controller;

        [SetUp]
        public void SetUp()
        {
            _mockKycDocService = new Mock<IKYCDocumentService>();
            _mockKycRepo       = new Mock<IKYCRecordRepository>();
            _mockEnv           = new Mock<IWebHostEnvironment>();

            _controller = new KYCDocumentsController(
                _mockKycDocService.Object,
                _mockKycRepo.Object,
                _mockEnv.Object);

            // Admin role bypasses the ownership check inside AuthorizeKycAccess.
            SetupControllerUser(_controller, userId: 1, role: "Admin");
        }

        // Operation tested: GetByKycId (READ)
        // Verifies that fetching KYC documents for a given KYC record ID returns HTTP 200.
        [Test]
        public async Task GetByKycId_ReturnsOk_WithDocumentList()
        {
            // Arrange
            int kycId = 5;

            // AuthorizeKycAccess calls _kycRepo.GetByIdAsync first.
            _mockKycRepo.Setup(r => r.GetByIdAsync(kycId))
                        .ReturnsAsync(new KYCRecord { KYCID = kycId, UserID = 1 });

            var docs = new List<KYCDocumentResponseDto>
            {
                new KYCDocumentResponseDto { KYCDocumentId = 1, KYCID = kycId },
                new KYCDocumentResponseDto { KYCDocumentId = 2, KYCID = kycId }
            };
            _mockKycDocService.Setup(s => s.GetByKycIdAsync(kycId))
                              .ReturnsAsync(docs);

            // Act
            var result = await _controller.GetByKycId(kycId);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult!.StatusCode, Is.EqualTo(200));
        }

        private static void SetupControllerUser(ControllerBase controller, int userId, string role)
        {
            var claims    = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Role, role)
            };
            var identity  = new ClaimsIdentity(claims, "TestAuth");
            var principal = new ClaimsPrincipal(identity);
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = principal }
            };
        }
    }
}

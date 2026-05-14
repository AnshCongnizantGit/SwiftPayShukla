using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SwiftPay.Controllers;
using SwiftPay.DTOs.RemittanceDTO;
using SwiftPay.DTOs.UserCustomerDTO;
using SwiftPay.Services.Interfaces;

namespace Swiftpay.Tests.Controllers
{
    [TestFixture]
    public class RemittancesControllerTests
    {
        private Mock<IRemittanceService>        _mockRemittanceService;
        private Mock<IDocumentService>          _mockDocumentService;
        private Mock<INotificationAlertService> _mockNotificationService;
        private Mock<ICustomerService>          _mockCustomerService;
        private Mock<IRefundRefService>         _mockRefundService;
        private RemittancesController           _controller;

        [SetUp]
        public void SetUp()
        {
            _mockRemittanceService    = new Mock<IRemittanceService>();
            _mockDocumentService      = new Mock<IDocumentService>();
            _mockNotificationService  = new Mock<INotificationAlertService>();
            _mockCustomerService      = new Mock<ICustomerService>();
            _mockRefundService        = new Mock<IRefundRefService>();

            _controller = new RemittancesController(
                _mockRemittanceService.Object,
                _mockDocumentService.Object,
                _mockNotificationService.Object,
                _mockCustomerService.Object,
                _mockRefundService.Object);

            // HttpContext required so User.FindFirstValue() doesn't throw.
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
        }

        // Operation tested: Create (CREATE)
        // Verifies that a valid remittance creation request returns HTTP 200 with the new remittance.
        [Test]
        public async Task Create_ReturnsOk_WhenRemittanceCreatedSuccessfully()
        {
            // Arrange
            var dto = new CreateRemittanceDto
            {
                CustomerId     = 1,
                BeneficiaryId  = 2,
                SendAmount     = 500m,
                QuoteId        = "QT-001",
                PurposeCode    = "FAMILY",
                SourceOfFunds  = "SALARY",
                FromCurrency   = "GBP",
                ToCurrency     = "INR"
            };
            var response = new CreateRemittanceResponseDto
            {
                RemitId      = 100,
                CustomerId   = 1,
                FromCurrency = "GBP",
                ToCurrency   = "INR",
                SendAmount   = 500m,
                Status       = "Draft"
            };
            _mockRemittanceService.Setup(s => s.CreateAsync(It.IsAny<CreateRemittanceDto>()))
                                  .ReturnsAsync(response);
            _mockCustomerService.Setup(s => s.GetByIdAsync(It.IsAny<int>()))
                                .ReturnsAsync(new CustomerResponseDto());

            // Act
            var result = await _controller.Create(dto);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult!.StatusCode, Is.EqualTo(200));
        }
    }
}

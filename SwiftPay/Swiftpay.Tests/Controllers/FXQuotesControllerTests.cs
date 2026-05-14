using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SwiftPay.Controllers;
using SwiftPay.DTOs.FXQuoteDTO;
using SwiftPay.Services.Interfaces;

namespace Swiftpay.Tests.Controllers
{
    [TestFixture]
    public class FXQuotesControllerTests
    {
        private Mock<IFXQuoteService> _mockFxQuoteService;
        private FXQuotesController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockFxQuoteService = new Mock<IFXQuoteService>();
            _controller = new FXQuotesController(_mockFxQuoteService.Object);

            // CreateQuote reads User.FindFirstValue(ClaimTypes.NameIdentifier) — provide it.
            SetupControllerUser(_controller, userId: 1, role: "Customer");
        }

        // Operation tested: CreateQuote (CREATE)
        // Verifies that a valid FX quote request returns HTTP 200 with the generated quote.
        [Test]
        public async Task CreateQuote_ReturnsOk_WhenQuoteGeneratedSuccessfully()
        {
            // Arrange
            var request = new CreateQuoteRequestDto
            {
                FromCurrency = "GBP",
                ToCurrency   = "INR",
                SendAmount   = 1000m
            };
            var response = new FXQuoteResponseDto
            {
                QuoteId        = "QT-001",
                FromCurrency   = "GBP",
                ToCurrency     = "INR",
                SendAmount     = 1000m,
                ReceiverAmount = 104500m,
                OfferedRate    = 104.5m,
                Status         = "Active"
            };
            _mockFxQuoteService.Setup(s => s.GenerateQuoteAsync(It.IsAny<CreateQuoteRequestDto>()))
                               .ReturnsAsync(response);

            // Act
            var result = await _controller.CreateQuote(request);

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

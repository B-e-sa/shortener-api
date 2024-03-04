using Microsoft.AspNetCore.Mvc;
using Moq;
using Shortener.Controllers;
using Shortener.Controllers.ResponseHandlers.ErrorHandlers;
using Shortener.Controllers.ResponseHandlers.SuccessHandlers;
using Shortener.Models;
using Shortener.Services.Models;
using Xunit;

namespace Shortener.Tests.Controllers
{
    public class CreateUrlControllerTests : IDisposable
    {
        private readonly CreateUrlController _sut;
        private readonly Mock<ICreateUrlService> _mockCreateUrlService;

        public CreateUrlControllerTests()
        {
            _mockCreateUrlService = new Mock<ICreateUrlService>();

            _sut = new CreateUrlController(
                _mockCreateUrlService.Object
            );
        }

        public void Dispose()
        {
            _mockCreateUrlService.Reset();
            GC.SuppressFinalize(this);
        }

        [Fact]
        public async Task Handle_ValidUrl_ReturnsCreatedUrl()
        {
            // Arrange
            var dummyUrl = Faker.Internet.Url();
            var createdUrl = new Url
            {
                OriginalUrl = dummyUrl,
                ShortUrl = "abcd"
            };

            _mockCreateUrlService
                .Setup(x => x.Handle(It.IsAny<Url>()))
                .ReturnsAsync(createdUrl);

            var request = new CreateUrlRequest { OriginalUrl = dummyUrl };

            // Act
            var result = await _sut.Handle(request);

            // Assert
            var createdResult = Assert.IsType<CreatedResult>(result);

            var createdHandlerResult = Assert.IsType<CreatedHandler>(
                createdResult.Value
            );

            Assert.IsType<Url>(createdHandlerResult.Value);
        }

        [Fact]
        public async Task Handle_InvalidUrl_ReturnsBadRequest()
        {
            // Arrange
            var invalidUrl = Faker.Lorem.Sentence();
            var request = new CreateUrlRequest { OriginalUrl = invalidUrl };

            _sut.ModelState.AddModelError("OriginalUrl", "Bad request error");

            // Act
            var result = await _sut.Handle(request);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<BadRequestHandler>(badRequestResult.Value);
        }

        // TODO: implement case if user.id is not present
    }
}
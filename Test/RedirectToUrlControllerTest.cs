using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shortener.Controllers;
using Shortener.Controllers.ResponseHandlers.ErrorHandlers;
using Shortener.Models;
using Shortener.Services.Models;
using Xunit;

namespace Shortener.Test
{
    public class RedirectToUrlControllerTest : IDisposable
    {
        private readonly RedirectToUrlController _sut;
        private readonly Mock<IFindUrlByShortUrlService> _findUrlByShortUrlService;

        public RedirectToUrlControllerTest()
        {
            _findUrlByShortUrlService = new Mock<IFindUrlByShortUrlService>();

            _sut = new RedirectToUrlController(
                _findUrlByShortUrlService.Object
            );
        }

        public void Dispose()
        {
            _findUrlByShortUrlService.Reset();
            GC.SuppressFinalize(this);
        }

        [Fact]
        public async Task Handle_ExistentShortUrl_ReturnsFoundUrl()
        {
            // Arrange 
            var shortUrl = "abcd";
            var foundUrl = new Url
            {
                OriginalUrl = Faker.Internet.Url(),
                ShortUrl = shortUrl
            };

            _findUrlByShortUrlService
                .Setup(x => x.Handle(shortUrl))
                .ReturnsAsync(foundUrl);

            // Act
            var result = await _sut.Handle(shortUrl);

            // Assert
            Assert.IsType<RedirectResult>(result);
        }

        [Fact]
        public async Task Handle_InexistentShortUrl_ReturnsNotFound()
        {
            // Arrange 
            var shortUrl = "abcd";

            _findUrlByShortUrlService
                .Setup(x => x.Handle(shortUrl))
                .ReturnsAsync((Url?)null);

            // Act
            var result = await _sut.Handle(shortUrl);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.IsType<NotFoundHandler>(notFoundResult.Value);
        }

        [Fact]
        public async Task Handle_InvalidShortUrl_ReturnsBadRequest()
        {
            // Arrange 
            var invalidUrl = Faker.Lorem.Sentence();

            // Act
            var result = await _sut.Handle(invalidUrl);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<BadRequestHandler>(badRequestResult.Value);
        }
    }
}
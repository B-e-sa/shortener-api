using Microsoft.AspNetCore.Mvc;
using Moq;
using Shortener.Controllers;
using Shortener.Controllers.ResponseHandlers.ErrorHandlers;
using Shortener.Controllers.ResponseHandlers.SuccessHandlers;
using Shortener.Models;
using Shortener.Services.Models;
using Xunit;

namespace Shortener.Test
{
    public class DeleteUrlControllerTest : IDisposable
    {
        private readonly DeleteUrlController _sut;
        private readonly Mock<IDeleteUrlService> _mockDeleteUrlService;
        private readonly Mock<IFindUrlByIdService> _mockFindUrlByIdService;

        public DeleteUrlControllerTest()
        {
            _mockDeleteUrlService = new Mock<IDeleteUrlService>();
            _mockFindUrlByIdService = new Mock<IFindUrlByIdService>();

            _sut = new DeleteUrlController(
                _mockDeleteUrlService.Object,
                _mockFindUrlByIdService.Object
            );
        }

        public void Dispose()
        {
            _mockDeleteUrlService.Reset();
            _mockFindUrlByIdService.Reset();
            GC.SuppressFinalize(this);
        }

        [Fact]
        public async Task Handle_ExistentUrl_ReturnsFoundUrl()
        {
            // Arrange
            var urlId = Guid.NewGuid();
            var foundUrl = new Url { Id = urlId };

            _mockFindUrlByIdService
                .Setup(x => x.Handle(urlId))
                .ReturnsAsync(foundUrl);

            _mockDeleteUrlService
                .Setup(x => x.Handle(foundUrl))
                .ReturnsAsync(foundUrl);

            var request = new DeleteUrlRequest { Id = urlId.ToString() };

            // Act
            var result = await _sut.Handle(request);

            // Assert
            var deletedResult = Assert.IsType<OkObjectResult>(result);

            var deletedHandlerResult = Assert.IsType<SuccessHandler>(
                deletedResult.Value
            );
            
            Assert.IsType<Url>(deletedHandlerResult.Value);
        }

        [Fact]
        public async Task Handle_InexistentUrl_ReturnsNotFound()
        {
            // Arrange
            var urlId = Guid.NewGuid();

            _mockFindUrlByIdService
                .Setup(x => x.Handle(urlId))
                .ReturnsAsync((Url)null!);

            var request = new DeleteUrlRequest { Id = urlId.ToString() };

            // Act
            var result = await _sut.Handle(request);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.IsType<NotFoundHandler>(notFoundResult.Value);
        }

        [Fact]
        public async Task Handle_InvalidId_ReturnsBadRequest()
        {
            // Arrange
            var invalidId = Faker.Lorem.Sentence();

            _sut
                .ModelState
                .AddModelError("Id", "Bad request error");

            var request = new DeleteUrlRequest { Id = invalidId };

            // Act
            var result = await _sut.Handle(request);

            // Assert
            var badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<BadRequestHandler>(badRequestObjectResult.Value);
        }
    }
}
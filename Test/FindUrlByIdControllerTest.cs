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
    public class FindUrlByIdControllerTest : IDisposable
    {
        private readonly FindUrlByIdController _sut;
        private readonly Mock<IFindUrlByIdService> _findUrlByIdService;

        public FindUrlByIdControllerTest()
        {
            _findUrlByIdService = new Mock<IFindUrlByIdService>();

            _sut = new FindUrlByIdController(
                _findUrlByIdService.Object
            );
        }

        public void Dispose()
        {
            _findUrlByIdService.Reset();
            GC.SuppressFinalize(this);
        }

        [Fact]
        public async Task Handle_ExistentUrl_ReturnsFoundUrl()
        {
            // Arrange
            var urlId = Guid.NewGuid();
            var foundUrl = new Url { Id = urlId };

            _findUrlByIdService
                .Setup(x => x.Handle(urlId))
                .ReturnsAsync(foundUrl);

            var request = new FindUrlByIdRequest { Id = urlId.ToString() };

            // Act
            var result = await _sut.Handle(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var successHandler = Assert.IsType<SuccessHandler>(okResult.Value);
            Assert.IsType<Url>(successHandler.Value);
        }

        [Fact]
        public async Task Handle_InexistentUrl_ReturnsNotFound()
        {
            // Arrange
            var urlId = Guid.NewGuid();

            _findUrlByIdService
                .Setup(x => x.Handle(urlId))
                .ReturnsAsync((Url?)null);

            var request = new FindUrlByIdRequest { Id = urlId.ToString() };

            // Act
            var result = await _sut.Handle(request);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.IsType<NotFoundHandler>(notFoundResult.Value);
        }

        [Fact]
        public async Task Handle_InvalidUrl_ReturnsBadRequest()
        {
            // Arrange
            var invalidId = Faker.Lorem.Sentence();

            _sut
                .ModelState
                .AddModelError("Id", "Bad request error");

            var request = new FindUrlByIdRequest { Id = invalidId };

            // Act
            var result = await _sut.Handle(request);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
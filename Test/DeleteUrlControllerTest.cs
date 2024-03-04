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
    public class DeleteUrlControllerTest
    {
        [Fact]
        public async Task Handle_ExistentUrl_ReturnsFoundUrl()
        {
            // Arrange
            var mockDeleteUrlService = new Mock<IDeleteUrlService>();
            var mockFindUrlByIdService = new Mock<IFindUrlByIdService>();
            var urlId = Guid.NewGuid();
            var foundUrl = new Url
            {
                Id = urlId
            };

            mockFindUrlByIdService
                .Setup(x => x.Handle(urlId))
                .ReturnsAsync(foundUrl);

            mockDeleteUrlService
                .Setup(x => x.Handle(foundUrl))
                .ReturnsAsync(foundUrl);

            var request = new DeleteUrlRequest { Id = urlId.ToString() };
            var deleteUrlController = new DeleteUrlController(
                mockDeleteUrlService.Object,
                mockFindUrlByIdService.Object
            );

            // Act
            var result = await deleteUrlController.Handle(request);

            // Assert
            var deletedResult = Assert.IsType<OkObjectResult>(result);
            var deletedHandlerResult = Assert.IsType<SuccessHandler>(deletedResult.Value);
            Assert.IsType<Url>(deletedHandlerResult.Value);
        }

        [Fact]
        public async Task Handle_InexistentUrl_ReturnsNotFoundAsync()
        {
            // Arrange
            var mockDeleteUrlService = new Mock<IDeleteUrlService>();
            var mockFindUrlByIdService = new Mock<IFindUrlByIdService>();
            var urlId = Guid.NewGuid();

            mockFindUrlByIdService
                .Setup(x => x.Handle(urlId))
                .ReturnsAsync((Url)null!);

            var request = new DeleteUrlRequest { Id = urlId.ToString() };
            var deleteUrlController = new DeleteUrlController(
                mockDeleteUrlService.Object,
                mockFindUrlByIdService.Object
            );

            // Act
            var result = await deleteUrlController.Handle(request);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.IsType<NotFoundHandler>(notFoundResult.Value);
        }

        [Fact]
        public async Task Handle_InvalidId_ReturnsBadRequest()
        {
            // Arrange
            var mockDeleteUrlService = new Mock<IDeleteUrlService>();
            var mockFindUrlByIdService = new Mock<IFindUrlByIdService>();
            var invalidId = Faker.Lorem.Sentence();

            var request = new DeleteUrlRequest { Id = invalidId };
            var deleteUrlController = new DeleteUrlController(
                mockDeleteUrlService.Object,
                mockFindUrlByIdService.Object
            );

            deleteUrlController
                .ModelState
                .AddModelError("OriginalUrl", "Bad request error");

            // Act
            var result = await deleteUrlController.Handle(request);

            // Assert
            var badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<BadRequestHandler>(badRequestObjectResult.Value);
        }
    }
}
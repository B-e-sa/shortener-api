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
    public class CreateUrlControllerTests
    {
        [Fact]
        public async Task Handle_ValidUrl_ReturnsCreated()
        {
            // Arrange
            var dummyUrl = Faker.Internet.Url();
            var mockCreateUrlService = new Mock<ICreateUrlService>();
            var createdUrl = new Url
            {
                OriginalUrl = dummyUrl,
                ShortUrl = "abcd"
            };

            mockCreateUrlService
                .Setup(x => x.Handle(It.IsAny<Url>()))
                .ReturnsAsync(createdUrl);

            var request = new CreateUrlRequest { OriginalUrl = dummyUrl };
            var controller = new CreateUrlController(mockCreateUrlService.Object);

            // Act
            var result = await controller.Handle(request);

            // Assert
            var createdResult = Assert.IsType<CreatedResult>(result);
            var createdHandlerResult = Assert.IsType<CreatedHandler>(createdResult.Value);
            Assert.IsType<Url>(createdHandlerResult.Value);
        }

        [Fact]
        public async Task Handle_InvalidUrl_ReturnsBadRequest()
        {
            // Arrange
            var invalidUrl = Faker.Lorem.Sentence();
            var request = new CreateUrlRequest { OriginalUrl = invalidUrl };
            var createUrlServiceMock = new Mock<ICreateUrlService>();

            var controller = new CreateUrlController(createUrlServiceMock.Object);
            controller.ModelState.AddModelError("OriginalUrl", "Bad request error");

            // Act
            var result = await controller.Handle(request);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        // TODO: implement case if user.id is not present
    }
}
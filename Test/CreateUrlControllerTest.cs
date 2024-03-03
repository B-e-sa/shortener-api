using Microsoft.AspNetCore.Mvc;
using Moq;
using Shortener.Controllers;
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
            var createUrlServiceMock = new Mock<ICreateUrlService>();
            var createdUrl = new Url
            {
                OriginalUrl = dummyUrl,
                ShortUrl = "abcd"
            };

            createUrlServiceMock
                .Setup(x => x.Handle(It.IsAny<Url>()))
                .ReturnsAsync(createdUrl);

            var request = new CreateUrlRequest { OriginalUrl = dummyUrl };
            var controller = new CreateUrlController(createUrlServiceMock.Object);

            // Act
            var result = await controller.Handle(request);

            // Assert
            var createdResult = Assert.IsType<CreatedResult>(result);
            var createdHandlerResult = Assert.IsType<CreatedHandler>(createdResult.Value);
            Assert.IsType<Url>(createdHandlerResult.Value);
        }
    }
}
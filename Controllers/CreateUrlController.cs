using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Shortener.Controllers.ResponseHandlers.ErrorHandlers;
using Shortener.Controllers.ResponseHandlers.SuccessHandlers;
using Shortener.Models;
using Shortener.Services.Models;
using System.ComponentModel.DataAnnotations;

namespace Shortener.Controllers
{
    public class CreateUrlRequest
    {
        [Required]
        [Url]
        public string OriginalUrl { get; set; } = string.Empty;
    }

    [ApiController]
    [Route("/")]
    public class CreateUrlController : ControllerBase
    {
        private readonly ICreateUrlService _createUrlService;

        public CreateUrlController(ICreateUrlService createUrlService)
        {
            _createUrlService = createUrlService;
        }

        [HttpPost()]
        public async Task<IActionResult> Handle([FromBody] CreateUrlRequest req)
        {
            if (!ModelState.IsValid)
                return BadRequest(
                    new BadRequestHandler()
                        {
                            Message = "Invalid URL."
                        }
                );

            // TODO: Implement if url.UserId is null handling

            Url newUrl = new()
            {
                // TODO: implement linked user

                OriginalUrl = req.OriginalUrl
            };

            Url createdUrl = await _createUrlService.Handle(newUrl);

            return Created(
                nameof(req),
                new CreatedHandler(createdUrl)
            );
        }
    }
}
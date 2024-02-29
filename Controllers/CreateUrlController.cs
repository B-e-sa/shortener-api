using Microsoft.AspNetCore.Mvc;
using Shortener.Models;
using Shortener.Services;
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
        private readonly CreateUrlService _createUrlService;

        public CreateUrlController(CreateUrlService createUrlService)
        {
            _createUrlService = createUrlService;
        }

        [HttpPost()]
        public async Task<IActionResult> Handle([FromBody] CreateUrlRequest req)
        {
            if (req.OriginalUrl is null)
                return BadRequest(new { message = "Invalid URL." });

            // TODO: Implement if url.UserId is null handling

            Url newUrl = new()
            {
                // TODO: implement linked user

                OriginalUrl = req.OriginalUrl
            };

            Url createdUrl = await _createUrlService.Handle(newUrl);

            return Created(
                nameof(req),
                new
                {
                    createdUrl
                }
            );
        }
    }
}
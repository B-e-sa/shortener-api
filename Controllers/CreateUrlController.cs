using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Shortener.Models;
using Shortener.Services;

namespace Shortener.Controllers
{
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
        public async Task<IActionResult> Handle([FromBody] Url url)
        {
            if (url.OriginalUrl is null)
                return BadRequest(new { message = "Invalid URL." });

            // TODO: Implement if url.UserId is null handling

            Url createdUrl = await _createUrlService.Handle(url);

            return Created(
                nameof(url),
                new
                {
                    createdUrl
                }
            );
        }
    }
}
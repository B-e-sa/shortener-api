using Microsoft.AspNetCore.Mvc;
using Shortener.Models;
using Shortener.Services;

namespace Shortener.Controllers
{
    [ApiController]
    [Route("/")]
    public class FindUrlByShortUrlController : ControllerBase
    {
        private readonly FindUrlByShortUrlService _findByUrlByShortUrlService;

        public FindUrlByShortUrlController(FindUrlByShortUrlService findUrlByShortUrlService)
        {
            _findByUrlByShortUrlService = findUrlByShortUrlService;
        }

        [HttpGet()]
        public async Task<IActionResult> Handle([FromBody] UrlDto urlDto)
        {
            if (urlDto.ShortUrl is null)
                return BadRequest(new { message = "Invalid URL." });

            Url? foundUrl = await _findByUrlByShortUrlService.Handle(urlDto.ShortUrl);

            if (foundUrl is null)
                return NotFound(new { message = "URL does not exists." });

            return Ok(
                new
                {
                    foundUrl
                }
            );
        }
    }
}


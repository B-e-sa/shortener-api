using Microsoft.AspNetCore.Mvc;
using Shortener.Services;
using Shortener.Models;

namespace Shortener.Controllers
{
    [ApiController]
    [Route("/")]
    public class RedirectToUrlController : ControllerBase
    {
        private readonly FindUrlByShortUrlService _findUrlByShortUrlService;

        public RedirectToUrlController(FindUrlByShortUrlService findUrlByShortUrlService)
        {
            _findUrlByShortUrlService = findUrlByShortUrlService;
        }

        [HttpGet("{url}")]
        public async Task<IActionResult> Handle(string url)
        {
            if (url.Length > 4 || url.Length < 4)
                return BadRequest(new { message = "Invalid Url." });

            Url? foundUrl = await _findUrlByShortUrlService.Handle(url);

            if (foundUrl is null)
                return NotFound(new { message = "The URL does not exists." });

            return Redirect(foundUrl.OriginalUrl);
        }
    }
}
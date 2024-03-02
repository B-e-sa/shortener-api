using Microsoft.AspNetCore.Mvc;
using Shortener.Models;
using Shortener.Services.Models;

namespace Shortener.Controllers
{
    [ApiController]
    [Route("/")]
    public class RedirectToUrlController : ControllerBase
    {
        private readonly IFindUrlByShortUrlService _findUrlByShortUrlService;

        public RedirectToUrlController(IFindUrlByShortUrlService findUrlByShortUrlService)
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
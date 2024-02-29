using Microsoft.AspNetCore.Mvc;
using Shortener.Services;
using Shortener.Models;
using System.ComponentModel.DataAnnotations;

namespace Shortener.Controllers
{
    public class RedirectToUrlRequest
    {
        [MaxLength(4)]
        [MinLength(4)]
        public string Url { get; set; }
    }

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
        public async Task<IActionResult> Handle(RedirectToUrlRequest req)
        {
            Url? foundUrl = await _findUrlByShortUrlService.Handle(req.Url);

            if (foundUrl is null)
                return NotFound(new { message = "The URL does not exists." });

            return Redirect(foundUrl.OriginalUrl);
        }
    }
}
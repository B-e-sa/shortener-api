using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Shortener.Models;
using Shortener.Services;
namespace Shortener.Controllers;

[ApiController]
[Route("/")]
public class UrlController : ControllerBase
{
    private readonly UrlService _urlService;

    public UrlController(UrlService urlService)
    {
        _urlService = urlService;
    }

    // TODO: Implement CREATE URL
    [HttpPost()]
    public async Task<IActionResult> Add([FromBody] Url url)
    {
        if (url.OriginalUrl is null)
            return BadRequest(new { message = "Original URL required." });

        bool isUrlValid = Uri.TryCreate(url.OriginalUrl, UriKind.Absolute, out Uri? uriResult)
            && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

        if (!isUrlValid)
            return BadRequest(new { message = "Invalid URL." });

        // TODO: Implement if url.UserId is null handling

        await _urlService.Add(url);

        return Created(
            nameof(url),
            new
            {
                url
            }
        );
    }

    [HttpGet("{url}")]
    public async Task<IActionResult> Find(string url)
    {
        Url? foundUrl = await _urlService.Find(url);

        if (foundUrl is null)
            return NotFound(new { message = "Provided URL does not exists." });

        return Redirect(foundUrl.OriginalUrl);
    }

    // TODO: Implement DELETE URL
    [HttpDelete()]
    public IActionResult Delete([FromBody] string url)
    {
        return Ok(
            new
            {
                url
            }
        );
    }
}

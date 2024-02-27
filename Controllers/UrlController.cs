using System.Diagnostics;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

    private static bool VerifyUrl(string url)
    {
        bool isUrlValid = Uri.TryCreate(url, UriKind.Absolute, out Uri? uriResult)
            && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

        return isUrlValid;
    }

    [HttpPost()]
    public async Task<IActionResult> Add([FromBody] Url url)
    {
        if (url.OriginalUrl is null)
            return BadRequest(new { message = "Original URL required." });

        bool isUrlValid = VerifyUrl(url.OriginalUrl);

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
        Url? foundUrl = await _urlService.FindByShortUrl(url);

        if (foundUrl is null)
            return NotFound(new { message = "Provided URL does not exists." });

        return Redirect(foundUrl.OriginalUrl);
    }

    // TODO: Implement DELETE URL
    [HttpDelete()]
    public async Task<IActionResult> Delete([FromBody] string id)
    {
        Guid idToGuid;

        try
        {
            idToGuid = Guid.Parse(id);
        }
        catch (ArgumentNullException)
        {
            return BadRequest(new { message = "URL id cannot be null." });
        }
        catch (FormatException)
        {
            return BadRequest(new { message = "Malformed URL id." });
        }

        Url? foundUrl = await _urlService.FindById(idToGuid);

        if (foundUrl is null)
            return NotFound(new { message = "URL not found" });

        Url deletedUrl = await _urlService.Delete(foundUrl);

        return Ok(
            new
            {
                deletedUrl
            }
        );
    }
}

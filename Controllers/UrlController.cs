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

    private static bool VerifyUrl(string url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out Uri? uriResult)
            && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
    }

    [HttpPost()]
    public async Task<IActionResult> Add([FromBody] Url url)
    {
        if (url.OriginalUrl is null || !VerifyUrl(url.OriginalUrl))
            return BadRequest(new { message = "Invalid URL." });

        // TODO: Implement if url.UserId is null handling

        Url createdUrl = await _urlService.Add(url);

        return Created(
            nameof(url),
            new
            {
                createdUrl
            }
        );
    }

    [HttpGet()]
    public async Task<IActionResult> FindByShortUrl([FromBody] UrlDto urlDto)
    {
        // TODO: Implement error if URL is not valid
        if (urlDto is null || !VerifyUrl(urlDto.Url))
            return BadRequest(new { message = "Invalid URL." });

        Url? foundUrl = await _urlService.FindByShortUrl(urlDto.Url);

        if (foundUrl is null)
            return NotFound(new { message = "URL does not exists." });

        return Ok(
            new
            {
                foundUrl
            }
        );
    }

    [HttpGet("{url}")]
    public async Task<IActionResult> RedirectTo(string url)
    {
        Url? foundUrl = await _urlService.FindByShortUrl(url);

        if (foundUrl is null)
            return NotFound(new { message = "The URL does not exists." });

        return Redirect(foundUrl.OriginalUrl);
    }

    // TODO: Implement DELETE URL
    [HttpDelete()]
    public async Task<IActionResult> Delete([FromBody] UrlDto urlDto)
    {
        if (urlDto.Id is null)
            return BadRequest(new { message = "Invalid Id." });

        Guid idToGuid;

        try
        {
            idToGuid = Guid.Parse(urlDto.Id);
        }
        catch (Exception)
        {
            return BadRequest(new { message = "Provided id is not a valid Guid." });
        }

        Url? foundUrl = await _urlService.FindById(idToGuid);

        if (foundUrl is null)
            return NotFound(new { message = "URL not found." });

        Url deletedUrl = await _urlService.Delete(foundUrl);

        return Ok(
            new
            {
                deletedUrl
            }
        );
    }
}

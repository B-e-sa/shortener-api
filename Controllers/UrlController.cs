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
            return BadRequest(new { message = "Original Url required" });

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
    public IActionResult Find(string url)
    {
        return Ok(
                   new
                   {
                       url
                   }
               );
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

using Microsoft.AspNetCore.Mvc;
namespace Shortener.Controllers;

[ApiController]
[Route("/")]
public class UrlController : ControllerBase
{
    // TODO: Implement find url URL
    [HttpGet("{url}")]
    public IActionResult FindUrl(string url)
    {
        return Ok(
            new
            {
                url
            }
        );
    }

    // TODO: Implement CREATE URL
    [HttpPost()]
    public IActionResult CreateUrl([FromBody] string url)
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
    public IActionResult DeleteUrl([FromBody] string url)
    {
        return Ok(
            new
            {
                url
            }
        );
    }
}

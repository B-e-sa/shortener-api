using Microsoft.AspNetCore.Mvc;
using Shortener.Models;
using Shortener.Services;

namespace Shortener.Controllers
{
    [ApiController]
    [Route("/")]
    public class FindUrlByIdController : ControllerBase
    {
        private readonly FindUrlByIdService _findUrlByIdService;

        public FindUrlByIdController(FindUrlByIdService findUrlByIdService)
        {
            _findUrlByIdService = findUrlByIdService;
        }

        public async Task<IActionResult> Handle([FromBody] UrlDto urlDto)
        {
            if (urlDto.Id is null)
                return BadRequest(new { message = "Invalid URL." });

            Url? foundUrl = await _findUrlByIdService.Handle(Guid.Parse(urlDto.Id));

            return Ok(
                new
                {
                    foundUrl
                }
            );
        }
    }
}
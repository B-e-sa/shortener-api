using Microsoft.AspNetCore.Mvc;
using Shortener.Models;
using Shortener.Services;

namespace Shortener.Controllers
{
    [ApiController]
    [Route("/")]
    public class DeleteUrlController : ControllerBase
    {
        private readonly DeleteUrlService _deleteUrlService;
         private readonly FindUrlByIdService _findUrlByIdService;

        public DeleteUrlController(
            DeleteUrlService deleteUrlService,
            FindUrlByIdService findUrlByIdService
        )
        {
            _deleteUrlService = deleteUrlService;
            _findUrlByIdService = findUrlByIdService;
        }

        [HttpDelete()]
        public async Task<IActionResult> Handle([FromBody] UrlDto urlDto)
        {
            if (urlDto.Id is null)
                return BadRequest(new { message = "Invalid Id." });

            Url? foundUrl = await _findUrlByIdService.Handle(Guid.Parse(urlDto.Id));

            if (foundUrl is null)
                return NotFound(new { message = "URL not found." });

            Url deletedUrl = await _deleteUrlService.Handle(foundUrl);

            return Ok(
                new
                {
                    deletedUrl
                }
            );
        }
    }
}
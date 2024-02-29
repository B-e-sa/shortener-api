using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Shortener.Models;
using Shortener.Services;

namespace Shortener.Controllers
{
    public class FindUrlByIdRequest
    {
        [RegularExpression(@"(?im)^[{(]?[0-9A-F]{8}[-]?(?:[0-9A-F]{4}[-]?){3}[0-9A-F]{12}[)}]?$")]
        public string Id { get; set; } = string.Empty;
    }

    [ApiController]
    [Route("/")]
    public class FindUrlByIdController : ControllerBase
    {
        private readonly FindUrlByIdService _findUrlByIdService;

        public FindUrlByIdController(FindUrlByIdService findUrlByIdService)
        {
            _findUrlByIdService = findUrlByIdService;
        }

        [HttpGet("url")]
        public async Task<IActionResult> Handle([FromBody] FindUrlByIdRequest req)
        {
            if (req.Id is null)
                return BadRequest(new { message = "Invalid id." });

            Url? foundUrl = await _findUrlByIdService.Handle(Guid.Parse(req.Id));

            if (foundUrl is null)
                return NotFound(new { message = "Url not found." });

            return Ok(
                new
                {
                    foundUrl
                }
            );
        }
    }
}
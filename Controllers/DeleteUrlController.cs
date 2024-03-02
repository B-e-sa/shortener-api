using Microsoft.AspNetCore.Mvc;
using Shortener.Models;
using Shortener.Services.Models;
using System.ComponentModel.DataAnnotations;

namespace Shortener.Controllers
{
    public class DeleteUrlRequest
    {
        [RegularExpression(@"(?im)^[{(]?[0-9A-F]{8}[-]?(?:[0-9A-F]{4}[-]?){3}[0-9A-F]{12}[)}]?$")]
        public string Id { get; set; } = string.Empty;
    }

    [ApiController]
    [Route("/")]
    public class DeleteUrlController : ControllerBase
    {
        private readonly IDeleteUrlService _deleteUrlService;
        private readonly IFindUrlByIdService _findUrlByIdService;

        public DeleteUrlController(
            IDeleteUrlService deleteUrlService,
            IFindUrlByIdService findUrlByIdService
        )
        {
            _deleteUrlService = deleteUrlService;
            _findUrlByIdService = findUrlByIdService;
        }

        [HttpDelete()]
        public async Task<IActionResult> Handle([FromBody] DeleteUrlRequest req)
        {
            if (req.Id is null)
                return BadRequest(new { message = "Invalid Id." });

            Url? foundUrl = await _findUrlByIdService.Handle(Guid.Parse(req.Id));

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
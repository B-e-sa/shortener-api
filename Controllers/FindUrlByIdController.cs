using Microsoft.AspNetCore.Mvc;
using Shortener.Controllers.ResponseHandlers.SuccessHandlers;
using Shortener.Models;
using Shortener.Services.Models;
using System.ComponentModel.DataAnnotations;

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
        private readonly IFindUrlByIdService _findUrlByIdService;

        public FindUrlByIdController(IFindUrlByIdService findUrlByIdService)
        {
            _findUrlByIdService = findUrlByIdService;
        }

        [HttpGet("url")]
        public async Task<IActionResult> Handle([FromBody] FindUrlByIdRequest req)
        {
            Url? foundUrl = await _findUrlByIdService.Handle(Guid.Parse(req.Id));

            if (foundUrl is null)
                return NotFound(
                    new 
                    { 
                        Message = "Searched URL not found." 
                    }
                );

            return Ok(new SuccessHandler(foundUrl));
        }
    }
}
using System.Net;

namespace Shortener.Controllers.ResponseHandlers.ErrorHandlers
{
    public class BadRequestHandler : ErrorHandler
    {
        public BadRequestHandler(
            string title = "Bad Request.", 
            string message = "Invalid user request.", 
            HttpStatusCode statusCode = HttpStatusCode.NotFound
        ) : base(title, message, statusCode)
        {
        }
    }
}
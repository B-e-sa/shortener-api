using System.Net;

namespace Shortener.Controllers.ResponseHandlers.ErrorHandlers
{
    public class NotFoundHandler : ErrorHandler
    {
        public NotFoundHandler(
            string title = "Not found.", 
            string message = "The searched resource was not found.", 
            HttpStatusCode statusCode = HttpStatusCode.NotFound
        ) : base(title, message, statusCode)
        {
        }
    }
}
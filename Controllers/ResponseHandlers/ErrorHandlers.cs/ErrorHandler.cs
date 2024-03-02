using System.Net;

namespace Shortener.Controllers.ResponseHandlers.ErrorHandlers
{
    public class ErrorHandler : ResponseHandler
    {
        public ErrorHandler(
            string title = "Error", 
            string message = "Some error occurred.", 
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError
        ) : base(title, message, statusCode)
        {
        }
    }
}
using System.Net;

namespace Shortener.Controllers.ResponseHandlers.SuccessHandlers
{
    public class CreatedHandler : SuccessHandler
    {
        public CreatedHandler(
            object? value,
            string title = "Created.", 
            string message = "Entity created with success.", 
            HttpStatusCode statusCode = HttpStatusCode.Created
        ) : base(value, title, message, statusCode)
        {
        }
    }
}
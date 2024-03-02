using System.Net;

namespace Shortener.Controllers.ResponseHandlers.SuccessHandlers
{
    public class SuccessHandler : ResponseHandler
    {
        public SuccessHandler(
            object? value,
            string title = "Success.", 
            string message = "Entity created with success.", 
            HttpStatusCode statusCode = HttpStatusCode.OK
        ) : base(title, message, statusCode)
        {
            Value = value;
        }

        public object? Value { get; set; }
    }
}
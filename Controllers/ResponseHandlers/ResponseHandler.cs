using System.Net;

namespace Shortener.Controllers.ResponseHandlers
{
    public class ResponseHandler
    {
        public ResponseHandler(
            string title, 
            string message, 
            HttpStatusCode statusCode
        )
        {
            Title = title;
            Message = message;
            StatusCode = statusCode;
        }

        public string Title { get; set; }

        public string Message { get; set; }

        public HttpStatusCode StatusCode { get; set; }
    }
}
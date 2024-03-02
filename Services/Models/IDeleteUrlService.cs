using Shortener.Models;

namespace Shortener.Services.Models
{
    public interface IDeleteUrlService
    {
        public Task<Url> Handle(Url url);
    }
}
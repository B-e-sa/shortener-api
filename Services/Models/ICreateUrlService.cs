using Shortener.Models;

namespace Shortener.Services.Models
{
    public interface ICreateUrlService
    {
        public Task<Url> Handle(Url url);
    }
}
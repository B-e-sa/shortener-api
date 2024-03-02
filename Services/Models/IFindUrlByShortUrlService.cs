using Shortener.Models;
using Shortener.Repositories.Models;

namespace Shortener.Services.Models
{
    public interface IFindUrlByShortUrlService
    {
        public Task<Url?> Handle(string url);
    }
}
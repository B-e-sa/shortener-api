using Shortener.Models;
using Shortener.Repositories.Models;
using Shortener.Services.Models;

namespace Shortener.Services.Implementations
{
    public class FindUrlByShortUrlService : IFindUrlByShortUrlService
    {
        private readonly IUrlRepository _urlRepository;

        public FindUrlByShortUrlService(IUrlRepository urlRepository)
        {
            _urlRepository = urlRepository;
        }

        public async Task<Url?> Handle(string url) => await _urlRepository.FindByShortUrl(url);
    }
}
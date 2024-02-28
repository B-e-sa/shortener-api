using Shortener.Models;
using Shortener.Repositories.Models;

namespace Shortener.Services
{
    public class FindUrlByShortUrlService
    {
        private readonly IUrlRepository _urlRepository;

        public FindUrlByShortUrlService(IUrlRepository urlRepository)
        {
            _urlRepository = urlRepository;
        }

        public async Task<Url?> Handle(string url) => await _urlRepository.FindByShortUrl(url);
    }
}
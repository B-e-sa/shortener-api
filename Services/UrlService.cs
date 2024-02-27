using Shortener.Models;
using Shortener.Repositories.Models;

namespace Shortener.Services
{
    public class UrlService
    {
        private readonly IUrlRepository _urlRepository;

        public UrlService(IUrlRepository urlRepository)
        {
            _urlRepository = urlRepository;
        }

        public async Task<Url> Add(Url url)
        {
            string shortUrl = "";
            string chars = "abcdefghijklmnopqrstuvwxyz0123456789";

            Random random = new();
            for (int i = 0; i < 4; i++)
            {
                int index = random.Next(0, chars.Length);
                shortUrl += chars[index];
            }

            url.ShortUrl = shortUrl;

            Url createdUrl = await _urlRepository.Add(url);

            return createdUrl;
        }

        public async Task<Url?> FindByShortUrl(string url) => await _urlRepository.FindByShortUrl(url);

        public async Task<Url?> FindById(Guid id) => await _urlRepository.FindById(id);

        public async Task<Url> Delete(Url url) => await _urlRepository.Delete(url);
    }
}
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

            await _urlRepository.Add(url);

            return url;
        }

        public string Find()
        {
            return "";
        }

        public string Delete()
        {
            return "";
        }
    }
}
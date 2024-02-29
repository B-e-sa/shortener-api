using Shortener.Models;
using Shortener.Repositories.Models;

namespace Shortener.Services
{
    public class CreateUrlService
    {
        private readonly IUrlRepository _urlRepository;

        public CreateUrlService(IUrlRepository urlRepository)
        {
            _urlRepository = urlRepository;
        }

        public async Task<Url> Handle(Url url)
        {
            string shortUrl = "";
            string chars = "abcdefghijklmnopqrstuvwxyz0123456789";

            Random random = new();
            for (int i = 0; i < 4; i++)
            {
                int index = random.Next(0, chars.Length);
                shortUrl += chars[index];
            }

            // TODO: implement linked user

            url.ShortUrl = shortUrl;
            url.CreatedAt = DateTime.Now;

            Url createdUrl = await _urlRepository.Add(url);

            return createdUrl;
        }
    }
}
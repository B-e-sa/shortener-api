using Microsoft.AspNetCore.SignalR;
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

        public async Task<Url> Add(Url url) => await _urlRepository.Add(url);

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
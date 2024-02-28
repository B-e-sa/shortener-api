using Shortener.Models;
using Shortener.Repositories.Models;

namespace Shortener.Services
{
    public class DeleteUrlService
    {
        private readonly IUrlRepository _urlRepository;

        public DeleteUrlService(IUrlRepository urlRepository)
        {
            _urlRepository = urlRepository;
        }

        public async Task<Url> Handle(Url url) => await _urlRepository.Delete(url);
    }
}
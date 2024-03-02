using Shortener.Models;
using Shortener.Repositories.Models;
using Shortener.Services.Models;

namespace Shortener.Services.Implementations
{
    public class DeleteUrlService : IDeleteUrlService
    {
        private readonly IUrlRepository _urlRepository;

        public DeleteUrlService(IUrlRepository urlRepository)
        {
            _urlRepository = urlRepository;
        }

        public async Task<Url> Handle(Url url) => await _urlRepository.Delete(url);
    }
}
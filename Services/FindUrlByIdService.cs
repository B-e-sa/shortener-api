using Shortener.Models;
using Shortener.Repositories.Models;

namespace Shortener.Services
{
    public class FindUrlByIdService
    {
        private readonly IUrlRepository _urlRepository;

        public FindUrlByIdService(IUrlRepository urlRepository)
        {
            _urlRepository = urlRepository;
        }

        public async Task<Url?> Handle(Guid id) => await _urlRepository.FindById(id);
    }
}
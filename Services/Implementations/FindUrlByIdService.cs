using Shortener.Models;
using Shortener.Repositories.Models;
using Shortener.Services.Models;

namespace Shortener.Services.Implentations
{
    public class FindUrlByIdService : IFindUrlByIdService
    {
        private readonly IUrlRepository _urlRepository;

        public FindUrlByIdService(IUrlRepository urlRepository)
        {
            _urlRepository = urlRepository;
        }

        public async Task<Url?> Handle(Guid id) => await _urlRepository.FindById(id);
    }
}
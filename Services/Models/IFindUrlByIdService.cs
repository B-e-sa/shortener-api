using Shortener.Models;
using Shortener.Repositories.Models;

namespace Shortener.Services.Models
{
    public interface IFindUrlByIdService
    {
        public Task<Url?> Handle(Guid id);
    }
}
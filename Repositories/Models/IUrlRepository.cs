using Shortener.Models;

namespace Shortener.Repositories.Models
{
    public interface IUrlRepository
    {
        Task<Url> Add(Url url);
        Task<Url?> FindByShortUrl(string url);
        Task<Url> Delete(Url url);
        Task<Url?> FindByUserId(Guid id);
        Task<Url?> FindById(Guid id);
    }
}

using Shortener.Models;

namespace Shortener.Repositories.Models
{
    public interface IUrlRepository
    {
        Task<Url> Add(Url url);
        Task<Url?> Find(string url);
        Task<Url?> Delete(string url);
        Task<Url?> FindByUserId(Guid id);
    }
}

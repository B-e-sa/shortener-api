using Shortener.Data;
using Shortener.Models;
using Shortener.Repositories.Models;

namespace Shortener.Repositories
{
    public class UrlRepository : IUrlRepository
    {
        private readonly ShortenerDbContext _dbContext;

        public UrlRepository(ShortenerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Url> Add(Url url)
        {
             _dbContext.Add(url);
            await _dbContext.SaveChangesAsync();
            
            return url;
        }

        public Task<Url> Delete(string url)
        {
            throw new NotImplementedException();
        }

        public Task<Url> Find(string url)
        {
            throw new NotImplementedException();
        }

        public Task<Url> FindByUserId(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}

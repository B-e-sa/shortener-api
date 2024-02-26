using Microsoft.EntityFrameworkCore;
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
            _dbContext.Urls.Add(url);
            await _dbContext.SaveChangesAsync();

            return url;
        }

        public Task<Url?> Delete(string url)
        {
            throw new NotImplementedException();
        }

        public async Task<Url?> Find(string url)
        {
            Url foundUrl = await _dbContext.Urls
                .Where(u => u.ShortUrl == url)
                .FirstAsync();

            return foundUrl;
        }

        public Task<Url?> FindByUserId(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}

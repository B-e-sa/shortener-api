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

        public async Task<Url> Delete(Url url)
        {
            _dbContext.Urls.Remove(url);
            await _dbContext.SaveChangesAsync();

            return url;
        }

        public async Task<Url?> FindByShortUrl(string url)
        {
            return await _dbContext.Urls
                .Where(u => u.ShortUrl == url)
                .FirstOrDefaultAsync();
        }

        // TODO: Implement FindByUserId
        public Task<Url?> FindByUserId(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Url?> FindById(Guid id)
        {
            return await _dbContext.Urls
                .Where(u => u.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}

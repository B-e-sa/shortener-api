using Microsoft.EntityFrameworkCore;
using Shortener.Models;

namespace Shortener.Data
{
    public class ShortenerDbContext : DbContext
    {
        public ShortenerDbContext(DbContextOptions<ShortenerDbContext> options)
            : base(options)
        { }

        public DbSet<Url> Urls { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
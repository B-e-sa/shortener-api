using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shortener.Models
{
    [Table("urls")]
    public class Url
    {
        public Guid Id { get; set; }

        [StringLength(4, MinimumLength = 4)]
        [Column("short_url")]
        public string ShortUrl { get; set; }

        [Column("original_url")]
        public string OriginalUrl { get; set; }
        
        public int Visits { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
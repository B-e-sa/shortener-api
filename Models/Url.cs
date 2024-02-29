using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shortener.Models
{
    [Table("urls")]
    [Index(nameof(ShortUrl), IsUnique = true)]
    public class Url
    {
        public Guid Id { get; set; }

        [Column("short_url", TypeName = "VARCHAR")]
        [StringLength(4, MinimumLength = 4)]
        [Required]
        public string ShortUrl { get; set; } = string.Empty;

        [Column("original_url")]
        [Required]
        [Url]
        public string OriginalUrl { get; set; } = string.Empty;

        [DefaultValue(0)]
        public int Visits { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
    }
}
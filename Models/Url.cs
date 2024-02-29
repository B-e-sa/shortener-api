using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Shortener.Models
{
    [Table("urls")]
    [Index(nameof(ShortUrl), IsUnique = true)]
    public class Url
    {
        public Guid Id { get; set; }

        [Column("short_url")]
        [StringLength(4, MinimumLength = 4)]
        [Required]
        public string? ShortUrl { get; set; }

        [Column("original_url")]
        [Required]
        [Url]
        public string OriginalUrl { get; set; }

        [DefaultValue(0)]
        public int Visits { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; }
    }
}
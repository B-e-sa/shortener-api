using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Shortener.Models
{
    [Table("users")]
    [Index(nameof(Email), IsUnique = true)]
    public class User
    {
        public Guid Id { get; set; }

        [MaxLength(24)]
        [MinLength(4)]
        [Required]
        public string Username { get; set; } = string.Empty;

        [EmailAddress]
        [Required]
        public string Email { get; set; } = string.Empty;

        /*
          * Minimum eight characters
          * At least one uppercase letter
          * At least one uppercase letter
          * One lowercase letter
          * One number and
          * One special character
        */
        [RegularExpression(
            @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$"
        )]
        public string Password { get; set; } = string.Empty;

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace Shortener.Models
{
    public class UrlDto
    {
        [RegularExpression(
            @"^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$",
            ErrorMessage = "Invalid id."
        )]
        public string? Id { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string? OriginalUrl { get; set; }

        [MaxLength(4, ErrorMessage = "The url must be a string and have a maximum of 4 characters")]
        public string? ShortUrl { get; set; }
    }
}

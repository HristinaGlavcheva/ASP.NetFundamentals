using System.ComponentModel.DataAnnotations;
using static Library.Data.DataConstants;

namespace Library.Data.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(AuthorMaxLength)]
        public string Author { get; set; } = string.Empty;

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public string ImageUrl { get; set; } = string.Empty;

        [Required]
        [Range(0.00, RatingMaxValue)]
        public decimal Rating { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public Category Category { get; set; } = null!;

        public IEnumerable<ApplicationUserBook> ApplicatoinUserBooks { get; set; } = new List<ApplicationUserBook>();
    }
}

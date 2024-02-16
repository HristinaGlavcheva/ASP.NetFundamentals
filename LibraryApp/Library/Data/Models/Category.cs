using System.ComponentModel.DataAnnotations;
using static Library.Data.DataConstants;

namespace Library.Data.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(CategoryNameMaxLength)]
        public string Name { get; set; } = String.Empty;

        public IEnumerable<Book> Books { get; set; } = new List<Book>();
    }
}
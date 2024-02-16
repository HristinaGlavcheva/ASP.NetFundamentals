using System.ComponentModel.DataAnnotations;
using static Library.Data.DataConstants;

namespace Library.ViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(CategoryNameMaxLength, MinimumLength = CategoryNameMinLength)]
        public string Name { get; set; } = string.Empty;
    }
}

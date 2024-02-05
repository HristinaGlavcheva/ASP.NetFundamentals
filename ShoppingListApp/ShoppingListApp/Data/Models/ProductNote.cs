using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingListApp.Data.Models
{
    public class ProductNote
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Content { get; set; } = String.Empty;

        public int ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; } = null!;
    }
}
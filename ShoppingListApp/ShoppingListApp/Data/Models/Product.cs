using System.ComponentModel.DataAnnotations;

namespace ShoppingListApp.Data.Models
{
    public class Product
    {
        public Product()
        {
            ProductNote = new List<ProductNote>();
            Name = string.Empty;
        }
        
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public IList<ProductNote> ProductNote { get; set; }
    }
}

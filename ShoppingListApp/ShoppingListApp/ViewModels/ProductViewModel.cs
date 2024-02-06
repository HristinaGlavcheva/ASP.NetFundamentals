using System.ComponentModel.DataAnnotations;

namespace ShoppingListApp.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="The name of product is required.")]
        [Display(Name="Product Name")]
        [StringLength(100, MinimumLength = 3, ErrorMessage ="Product name should be between 3 and 100 characters long.")]
        public string Name { get; set; } = string.Empty;
    }
}

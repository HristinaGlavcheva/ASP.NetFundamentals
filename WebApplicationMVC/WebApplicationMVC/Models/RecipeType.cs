using System.ComponentModel.DataAnnotations;

namespace WebApplicationMVC.Models
{
    public enum RecipeType
    {
        Unknown = 0,
        [Display(Name ="Fast food")]
        FastFood = 1,
        [Display(Name = "Long cooking meal")]
        LongCookingMeal = 2,
    }
}

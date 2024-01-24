using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApplicationMVC.Models;
using WebApplicationMVC.ValidationAttributes;

namespace WebApplicationMVC.ViewModels.Recipies
{
    public class AddRecipeInputModel
    {
        [MinLength(5)]
        [MaxLength(10)]
        [Required]
        [RegularExpression("[A-Z][^_]+", ErrorMessage = "Name should start with upper letter.")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public RecipeType Type { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "First time the recipe is cooked")]
        public DateTime FirstCooked { get; set; }

        [ValidDateValuesRange(1900)]
        public int Year => this.FirstCooked.Year;

        public IEnumerable<string> Ingredients { get; set; }

        public RecipeTimeInputModel Time { get; set; }

        public IFormFile Image { get; set; }
    }
}

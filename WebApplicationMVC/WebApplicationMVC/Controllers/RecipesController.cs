using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;
using WebApplicationMVC.ViewModels.Recipies; 

namespace WebApplicationMVC.Controllers
{ 
    public class RecipesController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;

        public RecipesController(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Add()
        {
            var model = new AddRecipeInputModel
            {
                Type = Models.RecipeType.Unknown,
                FirstCooked = DateTime.UtcNow,
                Time = new RecipeTimeInputModel
                {
                    CookingTime = 10,
                    PreparationTime = 5,
                }
            };
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddRecipeInputModel input)
        {
            if (!input.Image.FileName.EndsWith(".png"))
            {
                this.ModelState.AddModelError("Error", "Invalid file type.");
            }

            if(input.Image.Length > 10 * 1024 * 1024)
            {
                this.ModelState.AddModelError("Error", "File is too big.");
            }

            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            //TODO: Save data in DB

            //Save file in www.root:
            using (FileStream fs = new FileStream(this.webHostEnvironment.WebRootPath + "/user.png", FileMode.Create))
            {
                await input.Image.CopyToAsync(fs);
            }

            return this.RedirectToAction(nameof(ThankYou));
        }

        public IActionResult ThankYou()
        {
            return this.View(); 
        }
    }
}

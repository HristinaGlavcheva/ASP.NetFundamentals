using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using MVCIntroDemo.Models.Product;
using System.Text;
using System.Text.Json;

namespace MVCIntroDemo.Controllers
{
    public class ProductController : Controller
    {
        private IEnumerable<ProductViewModel> products =
             new List<ProductViewModel>()
        {
                new ProductViewModel()
                {
                    Id = 1,
                    Name = "Cheese",
                    Price = 7.00m
                },
                new ProductViewModel()
                {
                    Id = 2,
                    Name = "Ham",
                    Price = 5.50m
                },
                new ProductViewModel()
                {
                    Id=3,
                    Name = "Bread",
                    Price = 1.5m
                }
        };

        public IActionResult All(string? keyword)
        {
            if(keyword != null)
            {
                var filteredProducts = products
                    .Where(p => p.Name
                    .ToLower().Contains(keyword.ToLower()));

                return View(filteredProducts);
            }
            
            return View(products);
        }

        public IActionResult ById(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            
            if(product == null)
            {
                TempData["Error"] = "No such product";

                return RedirectToAction(nameof(All));
            }
            
            return View(product);
        }

        public IActionResult AllAsJson()
        {
            var options = new JsonSerializerOptions 
            {
                WriteIndented = true 
            };
            
            return Json(products, options);
        }

        public IActionResult AllAsText()
        {
            StringBuilder stringBuilder = GetAllAsString();

            return Content(stringBuilder.ToString());
        }

        public IActionResult AllAsTextFile()
        {
            Response.Headers.Add(HeaderNames.ContentDisposition, "attachment; text/plain");

            return File(Encoding.UTF8.GetBytes(GetAllAsString().ToString()), "text/plain");
        }

        private StringBuilder GetAllAsString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (var product in products)
            {
                stringBuilder.AppendLine($"Product {product.Id}: {product.Name} - {product.Price} lv.");
            }

            return stringBuilder;
        }
    }
}

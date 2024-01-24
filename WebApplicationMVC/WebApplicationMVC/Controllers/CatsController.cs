using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApplicationMVC.Models;

namespace WebApplicationMVC.Controllers
{
    public class CatsController : Controller
    {
        //[Route("/Dogs/All")]
        public IActionResult All()
        {
            var cats = new List<CatViewModel>
            {
                new CatViewModel
                {
                    Name = "Sharo",
                    Age = 6
                },
                new CatViewModel
                {
                    Name = "Lady",
                    Age = 14
                }
            };

            ViewData["Cats"] = "Kuchetata";

            return this.View(cats);
        }

        public IActionResult Create() => this.View();

        [HttpPost]
        public IActionResult Create(string name, int age)
        {
            var cat = new CatViewModel
            {
                Name = name,
                Age = age
            };

            return this.Redirect("/Cats/All");
        }
    }
}

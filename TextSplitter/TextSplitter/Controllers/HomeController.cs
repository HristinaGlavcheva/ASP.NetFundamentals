using Microsoft.AspNetCore.Mvc;
using TextSplitter.Models;

namespace TextSplitter.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(TextViewModel model)
        {
            return View(model);
        }

        [HttpPost]
        public IActionResult Split(TextViewModel model)
        {
            if(String.IsNullOrWhiteSpace(model.Text))
            {
                return View(nameof(Index), new TextViewModel());
            }

            var splittedText = model.Text
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .ToArray();

            model.SplitText = String.Join(Environment.NewLine, splittedText);
            
            return RedirectToAction(nameof(Index), model);
        }
    }
}


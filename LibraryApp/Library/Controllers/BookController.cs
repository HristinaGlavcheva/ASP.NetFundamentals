using Library.Data;
using Library.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.Controllers
{
    public class BookController : Controller
    {
        private readonly LibraryDbContext dbContext;

        public BookController(LibraryDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var model = await dbContext.Books
                .Select(b => new BookInfoViewModel
                {
                    Title = b.Title,
                    Author = b.Author,
                    ImageUrl = b.ImageUrl,
                    Category = b.Category.Name,
                    Rating = b.Rating.ToString()
                })
                .ToListAsync();

            return View(model);
        }
    }
}

using Library.Data;
using Library.Data.Models;
using Library.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Library.Controllers
{
    [Authorize]
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
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                    ImageUrl = b.ImageUrl,
                    Category = b.Category.Name,
                    Rating = b.Rating.ToString()
                })
                .ToListAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Mine()
        {
            var model = await dbContext.ApplicationUserBooks
                .AsNoTracking()
                .Where(ab => ab.ApplicationUserId == GetUserId())
                .Select(ab => new MyBooksViewModel
                {
                    Id = ab.Book.Id,
                    Title = ab.Book.Title,
                    Author = ab.Book.Author,
                    Description = ab.Book.Description,
                    Category = ab.Book.Category.Name,
                    ImageUrl = ab.Book.ImageUrl
                })
                .ToListAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCollection(int id)
        {
            var book = await dbContext.Books.FindAsync(id);

            if(book == null)
            {
                return BadRequest();
            }

            var userBook = new ApplicationUserBook
            {
                ApplicationUserId = GetUserId(),
                BookId = id,
            };

            //if (!dbContext.ApplicationUserBooks.Any(ab => ab.BookId == book.Id && ab.ApplicationUserId == GetUserId()))
            //{
            //    await dbContext.ApplicationUserBooks.AddAsync(userBook);
            //    await dbContext.SaveChangesAsync();
            //}

            if (!await dbContext.ApplicationUserBooks.ContainsAsync(userBook))
            {
                await dbContext.ApplicationUserBooks.AddAsync(userBook);
                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCollection(int id)
        {
            var book = await dbContext.Books
                .FindAsync(id);

            if(book == null)
            {
                return BadRequest();
            }

            var applicationUserBook = dbContext.ApplicationUserBooks
                .FirstOrDefault(ab => ab.ApplicationUserId == GetUserId() && ab.BookId == book.Id);

            if (applicationUserBook == null)
            {
                return BadRequest();
            }

            dbContext.Remove(applicationUserBook);
            await dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Mine));
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new BookFormModel();

            model.Categories = await GetCategories();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(BookFormModel model)
        {
            if (!ModelState.IsValid)
            {
                var categories = await GetCategories();

                return View(model);
            }

            var book = new Book
            {
                Title = model.Title,
                Author = model.Author,
                Description = model.Description,
                ImageUrl = model.Url,
                Rating = model.Rating,
                CategoryId = model.CategoryId
            };

            await dbContext.Books.AddAsync(book);
            await dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        private async Task<IEnumerable<CategoryViewModel>> GetCategories()
        {
            return await dbContext.Categories
                .AsNoTracking()
                .Select(c => new CategoryViewModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync ();
        }

        private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }
    }
}

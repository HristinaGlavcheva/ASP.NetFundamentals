using ForumApp.Core.Contracts;
using ForumApp.Core.Models;
using ForumApp.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace ForumApp.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService postService;

        public PostController(IPostService _postService)
        {
            postService = _postService;
        }

       [HttpGet]
       public async Task<IActionResult> All()
        {
            var model = await postService.GetAllAsync();

            return View(model);
        }

        [HttpGet]
        public IActionResult Add()
        {
            var model = new PostModel();
            
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(PostModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            await postService.AddAsync(model);

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await postService.GetByIdAsync(id);

            if (model == null)
            {
                ModelState.AddModelError("All", "Invalid post");
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PostModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            await postService.EditAsync(model);

            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await postService.DeleteAsync(id);

            return RedirectToAction(nameof(All));
        }
    }
}

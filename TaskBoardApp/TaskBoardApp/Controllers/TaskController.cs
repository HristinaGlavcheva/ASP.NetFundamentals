using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskBoardApp.Contracts;
using TaskBoardApp.ViewModels.Task;

namespace TaskBoardApp.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        private readonly ITaskService taskService;

        public TaskController(ITaskService _taskService)
        {
            taskService = _taskService;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new TaskFormModel();
            model.Boards = await taskService.GetBoardsAsync();
            
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TaskFormModel model)
        {
            var boards= await taskService.GetBoardsAsync();

            if(!boards.Any(b => b.Id == model.Id))
            {
                ModelState.AddModelError(nameof(model.BoardId), "Board does not exist");
            }

            if(!ModelState.IsValid)
            {
                model.Boards = boards;

                return View(model);
            }

            var userId = GetUserId();

            await taskService.CreateAsync(model, userId);

            return RedirectToAction("All", "Board");
        }

        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}

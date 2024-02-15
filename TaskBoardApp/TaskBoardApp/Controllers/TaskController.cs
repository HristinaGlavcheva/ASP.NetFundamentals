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

            if(!boards.Any(b => b.Id == model.BoardId))
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

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = await taskService.GetDetailsAsync(id);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var task = await taskService.GetByIdAsync(id);

            if(task == null)
            {
                return BadRequest();
            }
            
            if(task.OwnerId != GetUserId())
            {
                return Unauthorized();
            }

            var model = await taskService.ForEditAsync(task);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, TaskFormModel model)
        {
            var task = await taskService.GetByIdAsync(id);

            if (task == null)
            {
                return BadRequest();
            }

            if (task.OwnerId != GetUserId())
            {
                return Unauthorized();
            }

            var boards = await taskService.GetBoardsAsync();

            if (!boards.Any(b => b.Id == model.BoardId))
            {
                ModelState.AddModelError(nameof(model.BoardId), "Board does not exist");
            }

            if(!ModelState.IsValid)
            {
                model.Boards = boards;

                return View(model);
            }

            await taskService.EditAsync(model, id);
            return RedirectToAction("All", "Board");
        }

        [HttpGet]
        public async Task<IActionResult>Delete(int id)
        {
            var task = await taskService.GetByIdAsync(id);

            if (task == null)
            {
                return BadRequest();
            }

            if (task.OwnerId != GetUserId())
            {
                return Unauthorized();
            }

            var model = await taskService.ForDeleteAsync(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(TaskViewModel model)
        {
            var task = await taskService.GetByIdAsync(model.Id);

            if (task == null)
            {
                return BadRequest();
            }

            if (task.OwnerId != GetUserId())
            {
                return Unauthorized();
            }

            await taskService.DeleteAsync(model);

            return RedirectToAction("All", "Board");
        }

        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}

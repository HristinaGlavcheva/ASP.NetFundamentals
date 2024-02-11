using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskBoardApp.Contracts;

namespace TaskBoardApp.Controllers
{
    [Authorize]
    public class BoardController : Controller
    {
        private readonly IBoardService boardService;

        public BoardController(IBoardService _boardService)
        {
            boardService = _boardService;
        }

        public async Task<IActionResult> All()
        {
            var model = await boardService.GetAllAsync();

            return View(model);
        }
    }
}

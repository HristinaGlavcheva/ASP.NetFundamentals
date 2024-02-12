using Microsoft.EntityFrameworkCore;
using TaskBoardApp.Contracts;
using TaskBoardApp.Data;
using TaskBoardApp.ViewModels.Board;
using TaskBoardApp.ViewModels.Task;

namespace TaskBoardApp.Services
{
    public class BoardService : IBoardService
    {
        private readonly TaskBoardAppDbContext dbContext;

        public BoardService(TaskBoardAppDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public async Task<IEnumerable<BoardViewModel>> GetAllAsync()
        {
            var model = await dbContext
                .Boards
                .Select (b => new BoardViewModel
                {
                    Id = b.Id,
                    Name = b.Name,
                    Tasks = b.Tasks.Select(t => new TaskViewModel
                    {
                        Id = t.Id,
                        Title = t.Title,
                        Description= t.Descripion,
                        Owner = t.Owner.UserName
                    }).ToList()
                }).ToListAsync();

            return  model;
        }
    }
}

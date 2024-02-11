using Microsoft.EntityFrameworkCore;
using TaskBoardApp.Contracts;
using TaskBoardApp.Data;
using TaskBoardApp.ViewModels.Task;

namespace TaskBoardApp.Services
{
    public class TaskService : ITaskService
    {
        private readonly TaskBoardAppDbContext dbContext;

        public TaskService(TaskBoardAppDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public async Task CreateAsync(TaskFormModel model, string userId)
        {
            var task = new Data.Models.Task()
            {
                BoardId = model.BoardId,
                CreatedOn = DateTime.UtcNow,
                Descripion = model.Description,
                Title = model.Title,
                OwnerId = userId
            };
            
            await dbContext.Tasks.AddAsync(task);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<TaskBoardModel>> GetBoardsAsync()
        {
            var boards = await dbContext
                .Boards.AsNoTracking()
                .Select(b => new TaskBoardModel
                {
                    Id = b.Id,
                    Name = b.Name
                }).ToListAsync();

            return boards;
        }

        
    }
}

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

        public async Task DeleteAsync(TaskViewModel model)
        {
            var task = await dbContext.Tasks.Where(t => t.Id == model.Id).FirstOrDefaultAsync();

            dbContext.Tasks.Remove(task);
            await dbContext.SaveChangesAsync();
        }

        public async Task EditAsync(TaskFormModel model, int id)
        {
            var task = await GetByIdAsync(id);

            task.BoardId = model.BoardId;
            task.Title = model.Title;
            task.Descripion = model.Description;

            await dbContext.SaveChangesAsync();
        }

        public async Task<TaskViewModel> ForDeleteAsync(int id)
        {
            var task = await GetByIdAsync(id);

            var model = new TaskViewModel()
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Descripion
            };

            return model;
        }

        public async Task<TaskFormModel> ForEditAsync(Data.Models.Task task)
        {
            return new TaskFormModel
                {
                    BoardId = task.BoardId,
                    Title = task.Title,
                    Description = task.Descripion,
                    Boards = await GetBoardsAsync()
                };
        }

        public async Task<IEnumerable<TaskBoardModel>> GetBoardsAsync()
        {
            var boards = await dbContext
                .Boards
                .Select(b => new TaskBoardModel
                {
                    Id = b.Id,
                    Name = b.Name
                }).ToListAsync();

            return boards;
        }

        public async Task<Data.Models.Task> GetByIdAsync(int id)
        {
            var task = await dbContext.Tasks.FindAsync(id);

            return task;
        }

        public async Task<TaskDetailsViewModel> GetDetailsAsync(int id)
        {
            var model = await dbContext
                .Tasks
                .Where(t => t.Id == id)
                .Select(t => new TaskDetailsViewModel
                {
                    Board = t.Board.Name,
                    Description = t.Descripion,
                    CreatedOn = t.CreatedOn.ToString("dd.MM.yyyy HH:mm"),
                    Owner = t.Owner.UserName,
                    Title = t.Title
                })
                .FirstOrDefaultAsync();

            return model;
        }
    }
}

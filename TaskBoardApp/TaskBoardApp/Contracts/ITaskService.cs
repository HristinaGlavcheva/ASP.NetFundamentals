using TaskBoardApp.ViewModels.Task;

namespace TaskBoardApp.Contracts
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskBoardModel>> GetBoardsAsync();

        Task CreateAsync(TaskFormModel model, string userId);
    }
}

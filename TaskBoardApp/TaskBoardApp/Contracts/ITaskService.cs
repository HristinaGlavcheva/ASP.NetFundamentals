using TaskBoardApp.ViewModels.Task;

namespace TaskBoardApp.Contracts
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskBoardModel>> GetBoardsAsync();

        Task CreateAsync(TaskFormModel model, string userId);

        Task<TaskDetailsViewModel> GetDetailsAsync(int id);

        Task<Data.Models.Task> GetByIdAsync(int id);

        Task<TaskFormModel> ForEditAsync(Data.Models.Task task);

        Task EditAsync(TaskFormModel model, int id);

        Task<TaskViewModel> ForDeleteAsync(int id);

        Task DeleteAsync(TaskViewModel model);
    }
}

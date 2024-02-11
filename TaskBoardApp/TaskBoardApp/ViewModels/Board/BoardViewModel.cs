using System.ComponentModel.DataAnnotations;
using TaskBoardApp.ViewModels.Task;
using static TaskBoardApp.Data.DataConstants;

namespace TaskBoardApp.ViewModels.Board
{
    public class BoardViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(BoardNameMaxLength, MinimumLength = BoardNameMinLength)]
        public string Name { get; set; } = String.Empty;

        public ICollection<TaskViewModel> Tasks { get; set; } = new List<TaskViewModel>();
    }
}

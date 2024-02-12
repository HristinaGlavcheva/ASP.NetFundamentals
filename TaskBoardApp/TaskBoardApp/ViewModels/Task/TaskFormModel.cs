using System.ComponentModel.DataAnnotations;
using static TaskBoardApp.Data.DataConstants;

namespace TaskBoardApp.ViewModels.Task
{
    public class TaskFormModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(TaskTitleMaxLength,
            MinimumLength =TaskTitleMinLength,
            ErrorMessage="Field {2} should be between {2} and {1} characters long.")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(TaskDescriptionMaxLength, MinimumLength = TaskDescriptionMinLength, ErrorMessage ="Field {0} should be between {2} and {1} characters long.")]
        public string Description { get; set; } = string.Empty;

        [Display(Name ="Board")]
        public int BoardId { get; set; }

        public IEnumerable<TaskBoardModel> Boards { get; set; } = new List<TaskBoardModel>();
    }
}

using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static TaskBoardApp.Data.DataConstants;


namespace TaskBoardApp.ViewModels.Task
{
    public class TaskViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(TaskTitleMaxLength)]
        [MinLength(TaskTitleMinLength)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(TaskDescriptionMaxLength)]
        [MinLength(TaskDescriptionMinLength)]
        public string Description { get; set; } = string.Empty;

        public DateTime? CreatedOn { get; set; }

        public int? BoardId { get; set; }

        [Required]
        public string Owner { get; set; } = string.Empty;
    }
}

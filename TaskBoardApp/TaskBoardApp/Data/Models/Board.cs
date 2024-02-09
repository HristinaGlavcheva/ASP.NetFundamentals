using System.ComponentModel.DataAnnotations;
using static TaskBoardApp.Data.DataConstants;

namespace TaskBoardApp.Data.Models
{
    public class Board
    {
        public Board()
        {
            Tasks = new HashSet<Task>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(BoardNameMaxLength)]
        public string Name { get; set; } = String.Empty;

        public ICollection<Task> Tasks { get; set; }
    }
}

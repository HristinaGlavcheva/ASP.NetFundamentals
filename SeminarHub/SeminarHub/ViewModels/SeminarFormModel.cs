using System.ComponentModel.DataAnnotations;
using static SeminarHub.Data.DataConstants;

namespace SeminarHub.ViewModels
{
    public class SeminarFormModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(TopicMaxLength, MinimumLength = TopicMinLength)]
        public string Topic { get; set; } = string.Empty;

        [Required]
        [StringLength(LecturerMaxLength, MinimumLength = LecturerMinLength)]
        public string Lecturer { get; set; } = string.Empty;

        [Required]
        [StringLength(DetailsMaxLength, MinimumLength = DetailsMinLength)]
        public string Details { get; set; } = string.Empty;

        [Required]
        public DateTime DateAndTime { get; set; } = DateTime.Now;

        [Range(DurationMinValue, DurationMaxValue)]
        public int? Duration { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public IEnumerable<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();
    }
}

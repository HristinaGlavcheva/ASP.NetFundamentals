using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using static SeminarHub.Data.DataConstants;

namespace SeminarHub.Data.Models
{
    public class Seminar
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(TopicMaxLength)]
        public string Topic { get; set; } = string.Empty;

        [Required]
        [MaxLength(LecturerMaxLength)]
        public string Lecturer { get; set; } = string.Empty;


        [Required]
        [MaxLength(DetailsMaxLength)]
        public string Details { get; set; } = string.Empty;

        [Required]
        public string OrganizerId { get; set; } = string.Empty;

        [Required]
        public IdentityUser Organizer { get; set; } = null!;

        [Required]
        public DateTime DateAndTime { get; set; }

        [Range(DurationMinValue, DurationMaxValue)]
        public int? Duration { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public Category Category { get; set; } = null!;

        public IList<SeminarParticipant> SeminarParticipants { get; set; } = new List<SeminarParticipant>();
    }
}

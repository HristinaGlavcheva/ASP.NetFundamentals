using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Homies.Data.Models
{
    public class EventParticipant
    {
        [Required]
        public string HelperId { get; set; } = string.Empty;

        public IdentityUser Helper { get; set; } = null!;

        [Required]
        public int EventId { get; set; }

        public Event Event { get; set; } = null!;
    }
}

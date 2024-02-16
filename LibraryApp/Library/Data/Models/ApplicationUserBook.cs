using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Library.Data.Models
{
    public class ApplicationUserBook
    {
        [Key]
        [Required]
        public string ApplicationUserId { get; set; } = string.Empty;   

        public IdentityUser ApplicationUser { get; set; } = null!;

        [Key]
        [Required]
        public int BookId { get; set; }

        public Book Book { get; set; } = null!;
    }
}
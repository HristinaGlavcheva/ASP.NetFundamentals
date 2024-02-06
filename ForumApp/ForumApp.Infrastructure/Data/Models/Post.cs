using static ForumApp.Infrastructure.Constants.ValidationConstants;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ForumApp.Infrastructure.Data.Models
{
    [Comment("Posts table")]
    public class Post
    {
        [Key]
        [Comment("Post identifier")]
        public int MyProperty { get; init; }

        [Required]
        [MaxLength(TitleMaxLength)]
        [Comment("Post title")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(ContentMaxLength)]
        [Comment("Post content")]
        public string Content { get; set; } = string.Empty;
    }
}

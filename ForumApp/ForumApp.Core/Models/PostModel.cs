using System.ComponentModel.DataAnnotations;
using static ForumApp.Infrastructure.Constants.ValidationConstants;

namespace ForumApp.Core.Models
{
    /// <summary>
    /// Post data transfer model
    /// </summary>
    public class PostModel
    {
        /// <summary>
        /// Post identifier
        /// </summary>
        public int Id { get; init; }

        /// <summary>
        /// Post title
        /// </summary>
        [Required(ErrorMessage = RequireErrorMesage)]
        [StringLength(TitleMaxLength,
            MinimumLength = TitleMinLength,
            ErrorMessage = StringLengthErrorMessage)]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Post content
        /// </summary>
        [Required(ErrorMessage = RequireErrorMesage)]
        [StringLength(ContentMaxLength,
            MinimumLength = ContentMinLength,
            ErrorMessage = StringLengthErrorMessage)]
        public string Content { get; set; } = string.Empty;
    }
}

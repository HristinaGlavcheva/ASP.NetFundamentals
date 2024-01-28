using System.ComponentModel.DataAnnotations;

namespace TextSplitter.Models
{
    public class TextViewModel
    {
        [Required(ErrorMessage ="Here you may enter your text to be splitted.")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Your text should be between 2 and 30 characters long.")]
        public string Text { get; set; } = string.Empty;

        public string SplitText { get; set; } = string.Empty;
    }
}

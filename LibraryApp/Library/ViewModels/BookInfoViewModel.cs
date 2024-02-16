namespace Library.ViewModels
{
    public class BookInfoViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = String.Empty;

        public string Author { get; set; } = String.Empty;

        public string Category { get; set; } = String.Empty;

        public string Rating { get; set; } = String.Empty;

        public string ImageUrl { get; set; } = String.Empty;  
    }
}

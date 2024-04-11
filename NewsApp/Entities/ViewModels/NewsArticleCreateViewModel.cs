namespace NewsApp.Entities.ViewModels
{
    public class NewsArticleCreateViewModel
    {
        public required string Title { get; set; }
        public required IFormFile HeaderImage { get; set; }
        public required string TextData { get; set; }
    }
}

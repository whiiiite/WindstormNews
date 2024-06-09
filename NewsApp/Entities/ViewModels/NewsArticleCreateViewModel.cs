namespace NewsApp.Entities.ViewModels
{
    /// <summary>
    /// Class for creationg data of News Article
    /// </summary>
    public class NewsArticleCreateViewModel
    {
        public required string Title { get; set; }
        public required IFormFile HeaderImage { get; set; }
        public required string CategoryId { get; set; }
        public required string TextData { get; set; }
    }
}

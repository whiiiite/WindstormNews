namespace NewsApp.Entities.ViewModels
{
    /// <summary>
    /// Class for contain edit data for News Article
    /// </summary>
    public class NewsArticleEditViewModel
    {
        public required string Title { get; set; }
        public string? ImagePath { get; set; }
        public IFormFile? Image { get; set; }
        public required string CategoryId { get; set; }
        public required string TextData { get; set; }
        public required bool IsDeleted { get; set; }
    }
}

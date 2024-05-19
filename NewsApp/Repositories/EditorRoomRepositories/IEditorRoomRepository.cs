using Microsoft.AspNetCore.Mvc.ModelBinding;
using NewsApp.Entities.Models;
using NewsApp.Entities.ViewModels;
using NewsApp.Shared;

namespace NewsApp.Repositories.EditorRoomRepositories
{
    public interface IEditorRoomRepository
    {
        public Task<IEnumerable<NewsArticle>> GetArticlesAsync(int pageSize, int page);
        public Task<NewsArticle> GetArticleAsync(string id);
        public Task<OperationResult> CreateArticleAsync(NewsArticleCreateViewModel newsArticleVM, ModelStateDictionary modelState, string userId);
        public Task<OperationResult> UpdateArticleAsync(string id, NewsArticleEditViewModel newsArticleVM);
        public Task<OperationResult> DeleteArticleAsync(string id);
    }
}

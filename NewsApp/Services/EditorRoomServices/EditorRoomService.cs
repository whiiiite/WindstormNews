using Microsoft.AspNetCore.Mvc.ModelBinding;
using NewsApp.Entities.Models;
using NewsApp.Entities.ViewModels;
using NewsApp.Repositories.EditorRoomRepositories;
using NewsApp.Shared;

namespace NewsApp.Services.EditorRoomServices
{
    public class EditorRoomService : IEditorRoomService
    {
        readonly IEditorRoomRepository editorRoomRepository;
        public EditorRoomService(IEditorRoomRepository editorRoomRepository)
        {
            this.editorRoomRepository = editorRoomRepository;
        }

        public async Task<OperationResult> CreateArticleAsync(NewsArticleCreateViewModel newsArticleVM, ModelStateDictionary modelState, string userId)
        {
            return await editorRoomRepository.CreateArticleAsync(newsArticleVM, modelState, userId);
        }

        public async Task<OperationResult> DeleteArticleAsync(string id)
        {
            return await editorRoomRepository.DeleteArticleAsync(id);
        }

        public async Task<NewsArticle> GetArticleAsync(string id)
        {
            return await editorRoomRepository.GetArticleAsync(id);
        }

        public async Task<IEnumerable<NewsArticle>> GetArticlesAsync(int pageSize, int page)
        {
            return await editorRoomRepository.GetArticlesAsync(pageSize, page);
        }

        public async Task<OperationResult> UpdateArticleAsync(string id, NewsArticleEditViewModel newsArticleVM)
        {
            return await editorRoomRepository.UpdateArticleAsync(id, newsArticleVM);
        }
    }
}

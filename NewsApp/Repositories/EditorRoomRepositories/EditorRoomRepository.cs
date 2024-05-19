using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using NewsApp.Data;
using NewsApp.Entities.Models;
using NewsApp.Entities.ViewModels;
using NewsApp.Extentions;
using NewsApp.Shared;
using NewsApp.Shared.FileSystem;

namespace NewsApp.Repositories.EditorRoomRepositories
{
    public class EditorRoomRepository : IEditorRoomRepository
    {
        NewsAppContext _context;
        public EditorRoomRepository(NewsAppContext context) 
        {
            _context = context;
        }

        public async Task<OperationResult> CreateArticleAsync(NewsArticleCreateViewModel newsArticleVM, ModelStateDictionary modelState, string userId)
        {
            if (!Directory.Exists(Defaults.ArticleHeaderImagesPath))
            {
                Directory.CreateDirectory(Defaults.ArticleHeaderImagesPath);
            }

            IFormFile headerImage = newsArticleVM.HeaderImage;
            string saveImagePath = Path.Combine(Defaults.ArticleHeaderImagesPath, headerImage.FileName);
            await FileHelper.CopyFileAsync(newsArticleVM.HeaderImage, saveImagePath);

            NewsArticle newsArticle = new NewsArticle()
            {
                UserId = userId,
                Title = newsArticleVM.Title,
                TextData = newsArticleVM.TextData,
                HeadImagePath = saveImagePath,
                CategoryId = newsArticleVM.CategoryId,
                CreatedDate = DateTimeOffset.UtcNow,
                EditDate = DateTimeOffset.UtcNow,
                IsDeleted = false
            };

            if (modelState.IsValid)
            {
                _context.Add(newsArticle);
                await _context.SaveChangesAsync();
                return OperationResult.SuccessInstance;
            }

            return OperationResult.IsNotSuccess("Data is not valid");
        }

        public async Task<OperationResult> DeleteArticleAsync(string id)
        {
            if (_context.NewsArticle == null)
            {
                return OperationResult.IsNotSuccess("NewsArticle is null");
            }
            var newsArticle = await GetArticleAsync(id);
            if (newsArticle == null)
            {
                return OperationResult.IsNotSuccess("NewsArticle is not found");
            } 

            _context.NewsArticle.Remove(newsArticle);
            await _context.SaveChangesAsync();
            return OperationResult.SuccessInstance;
        }

        public async Task<NewsArticle?> GetArticleAsync(string id)
        {
            return await _context.NewsArticle.FindAsync(id);
        }

        public async Task<IEnumerable<NewsArticle>> GetArticlesAsync(int pageSize, int page)
        {
            return await _context.NewsArticle
                          .OrderByDescending(x => x.CreatedDate)
                          .Skip((page - 1) * pageSize)
                          .Take(pageSize)
                          .ToListAsync();
        }

        public Task<OperationResult> UpdateArticleAsync(string id, NewsArticleEditViewModel newsArticleVM)
        {
            throw new NotImplementedException();
        }
    }
}

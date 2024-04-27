using NewsApp.Entities.Models;
using System.Linq.Expressions;

namespace NewsApp.Repositories.NewsArticles
{
    public interface INewsArticlesRepository
    {
        public Task<IList<NewsArticle>> GetArticlesAsync(int pageSize, int page = 1);
        public Task<NewsArticle> GetArticleByIdAsync(string id);
    }
}

using NewsApp.Entities.Models;
using System.Linq.Expressions;

namespace NewsApp.Services.NewsArticles
{
    public interface INewsArticlesService
    {
        public Task<IList<NewsArticle>> GetArticlesAsync(int pageSize, int page = 1);
        public Task<NewsArticle> GetArticleByIdAsync(string id);
        public Task<long> CountArticlesAsync(Expression<Func<NewsArticle, bool>>? predicate);
    }
}

using Microsoft.EntityFrameworkCore;
using NewsApp.Data;
using NewsApp.Entities.Models;
using NewsApp.Repositories.NewsArticles;
using System.Drawing.Printing;
using System.Linq.Expressions;

namespace NewsApp.Services.NewsArticles
{
    public class NewsArticlesService : INewsArticlesService
    {
        readonly INewsArticlesRepository newsArticlesRepository;
        readonly NewsAppContext context;

        public NewsArticlesService(INewsArticlesRepository newsArticlesRepository, NewsAppContext context) 
        {
            this.context = context;
            this.newsArticlesRepository = newsArticlesRepository;
        }

        public async Task<NewsArticle?> GetArticleByIdAsync(string id)
        {
            return await newsArticlesRepository.GetArticleByIdAsync(id);
        }

        public async Task<IList<NewsArticle>> GetArticlesAsync(int pageSize, int page = 1)
        {
            return await newsArticlesRepository.GetArticlesAsync(pageSize, page);
        }

        public async Task<long> CountArticlesAsync(Expression<Func<NewsArticle, bool>>? predicate)
        {
            long count;
            if (predicate == null)
            {
                count = await context.NewsArticle.LongCountAsync();
            }
            else
            {
                count = await context.NewsArticle.LongCountAsync(predicate);
            }

            return count;
        }
    }
}

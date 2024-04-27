using Microsoft.EntityFrameworkCore;
using NewsApp.Data;
using NewsApp.Entities.Models;
using System.Drawing.Printing;
using System.Linq.Expressions;

namespace NewsApp.Repositories.NewsArticles
{
    public class NewsArticlesRepository : INewsArticlesRepository
    {
        readonly NewsAppContext context;
        public NewsArticlesRepository(NewsAppContext context)
        {
            this.context = context;
        }

        public async Task<NewsArticle?> GetArticleByIdAsync(string id)
        {
            var newsArticle = await context.NewsArticle
                .Include(n => n.User)
                .Include(n => n.Category)
                .FirstOrDefaultAsync(m => m.Id == id);

            return newsArticle;
        }

        public async Task<IList<NewsArticle>> GetArticlesAsync(int pageSize, int page = 1)
        {
            var newsAppContext = context.NewsArticle.Include(n => n.User)
            .Include(n => n.Category);

            return await newsAppContext.
                           Where(x => x.IsDeleted == false)
                          .OrderByDescending(x => x.CreatedDate)
                           .Skip((page - 1) * pageSize)
                          .Take(pageSize)
                          .ToListAsync();
        }

        public async Task<long> CountArticlesAsync(Expression<Func<NewsArticle, bool>>? predicate)
        {
            long count;
            if(predicate == null)
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

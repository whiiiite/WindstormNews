using NewsApp.Entities.Models;

namespace NewsApp.Repositories.NewsArticles
{
    public interface INewsArticlesRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="page"></param>
        /// <returns>Articles of range</returns>
        public Task<IList<NewsArticle>> GetArticlesAsync(int pageSize, int page = 1);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Article object by its id</returns>
        public Task<NewsArticle> GetArticleByIdAsync(string id);
    }
}

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NewsApp.Configurations;
using NewsApp.Entities.Models;

namespace NewsApp.Data
{
    public class NewsAppContext : IdentityDbContext<User>
    {
        public NewsAppContext (DbContextOptions<NewsAppContext> options)
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new NewsArticleConfiguration());
        }

        public DbSet<NewsArticle> NewsArticle { get; set; } = default!;
        public DbSet<Category> Categories { get; set; } = default!;
    }
}

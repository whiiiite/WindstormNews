using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewsApp.Entities.Models;

namespace NewsApp.Configurations
{
    public class NewsArticleConfiguration : IEntityTypeConfiguration<NewsArticle>
    {
        public void Configure(EntityTypeBuilder<NewsArticle> builder)
        {
            builder.Property(e => e.Id).IsRequired();
            builder.Property(e => e.Title).IsRequired();
            builder.Property(e => e.TextData).IsRequired();
            builder.Property(e => e.CreatedDate).IsRequired();
            builder.Property(e => e.EditDate).IsRequired();
            builder.Property(e => e.UserId).IsRequired();
            builder.Property(e => e.IsDeleted).HasDefaultValue(false);

            builder.HasOne(e => e.User)
                .WithMany(u => u.NewsArticles)
                .HasForeignKey(e => e.UserId)
                .IsRequired();
        }
    }
}

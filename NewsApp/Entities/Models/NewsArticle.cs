using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewsApp.Entities.Models
{
    public class NewsArticle
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public required string Title { get; set; }
        public required string HeadImagePath { get; set; }
        public required string TextData { get; set; }
        public required DateTimeOffset CreatedDate { get; set; }
        public required DateTimeOffset EditDate { get; set; }
        public required string CategoryId { get; set; }
        public Category Category { get; set; }
        /// <summary>
        /// Id of writer of article
        /// </summary>
        public string UserId { get; set; }
        public User User { get; set; }
        [DefaultValue(false)]
        public required bool IsDeleted { get; set; } 
    }
}

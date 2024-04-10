using Microsoft.AspNetCore.Identity;
using System.ComponentModel;

namespace NewsApp.Entities.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<NewsArticle> NewsArticles { get; set; }
        [DefaultValue(false)]
        public bool IsSuperUser { get; set; }
    }
}

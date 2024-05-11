using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewsApp.Entities.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class Category
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [DefaultValue(false)]
        public bool IsDeprecated { get; set; }
    }
}

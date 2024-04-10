using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewsApp.Entities.Models;

namespace NewsApp.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(e => e.FirstName).IsRequired();
            builder.Property(e => e.LastName).IsRequired();
            builder.Property(e => e.IsSuperUser).HasDefaultValue(false);
        }
    }
}

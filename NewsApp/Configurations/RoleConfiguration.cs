using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace NewsApp.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        var rolesNames = new List<string>()
        {
            "Simple", "Administrator", "Owner", "Donator",
            "Editor", "ChiefEditor"
        };

        var roles = new List<IdentityRole>();
        foreach (var roleName in rolesNames)
        {
            roles.Add(new IdentityRole()
            {
                Name = roleName,
                NormalizedName = roleName.ToUpper()
            }); 
        }

        builder.HasData(roles);
    }
}

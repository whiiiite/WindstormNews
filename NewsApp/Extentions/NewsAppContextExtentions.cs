using Microsoft.EntityFrameworkCore;
using NewsApp.Data;
using NewsApp.Entities.Models;
using System.Security.Principal;

namespace NewsApp.Extentions
{
    public static class NewsAppContextExtentions
    {
        public static async Task<User?> GetUserAsync(this NewsAppContext context, IIdentity? currentIdentity)
        {
            if (currentIdentity == null)
            {
                return null;
            }

            User? user = await context.Users
                        .Where(x => x.Email == currentIdentity.Name)
                        .FirstOrDefaultAsync();

            return user;
        }
    }
}

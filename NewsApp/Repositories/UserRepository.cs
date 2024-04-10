using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NewsApp.Data;
using NewsApp.Entities.Models;
using NewsApp.Extentions;
using System.Linq.Expressions;
using System.Security.Principal;
using NewsApp.Entities.ViewModels;

namespace NewsApp.Repositories
{
    public class UserRepository
    {
        readonly NewsAppContext context;
        readonly UserManager<User>? userManager;

        //public UserRepository(XstorageDbContext context)
        //{
        //    this.context = context;
        //}

        public UserRepository(NewsAppContext context, UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        /// <summary>
        /// Creates user in db
        /// </summary>
        /// <param name="context"></param>
        /// <param name="userManager"></param>
        /// <param name="userData"></param>
        /// <returns></returns>
        public async Task<IEnumerable<IdentityError>> CreateUserAsync(SignUpViewModel userData)
        {
            // todo: add mapping
            User user = new User();
            user.UserName = userData.UserName;
            user.Email = userData.Email;
            user.FirstName = string.Empty;
            user.LastName = string.Empty;
            IdentityResult res = await userManager.CreateAsync(user, userData.Password);

            if (!res.Errors.Any())
            {
                context.Users.Update(user);
                await context.SaveChangesAsync();

                user = await userManager.FindByEmailAsync(userData.Email);
            }

            return res.Errors;
        }


        /// <summary>
        /// Get user from db by identity
        /// </summary>
        /// <param name="context"></param>
        /// <param name="identity"></param>
        /// <returns></returns>
        public async Task<User?> GetUserAsync(IIdentity? identity)
        {
            if (identity == null)
            {
                throw new ArgumentNullException("IIdentity is null");
            }

            return await context.GetUserAsync(identity);
        }

        /// <summary>
        /// Get user id by its identity
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public async Task<string> GetUserIdAsync(IIdentity? identity)
        {
            if (identity == null)
            {
                throw new ArgumentNullException("IIdentity is null");
            }

            return (await context.GetUserAsync(identity))?.Id;
        }

        /// <summary>
        /// Finds user in db by predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<User?> GetUserAsync(Expression<Func<User, bool>> predicate)
        {
            return await context.Users.FirstOrDefaultAsync(predicate);
        }
    }
}

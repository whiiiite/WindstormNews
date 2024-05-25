using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NewsApp.Data;
using NewsApp.Entities.Models;
using NewsApp.Extentions;
using System.Linq.Expressions;
using System.Security.Principal;
using NewsApp.Entities.ViewModels;

namespace NewsApp.Repositories.Users
{
    /// <summary>
    /// Class that handles operations over data of User model and controller
    /// </summary>
    public class UserRepository : IUserRepository
    {
        readonly NewsAppContext context;
        readonly UserManager<User> userManager;

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
            IdentityResult identityResult = await userManager.CreateAsync(user, userData.Password);

            if (!identityResult.Errors.Any())
            {
                context.Users.Update(user);
                await context.SaveChangesAsync();
            }

            return identityResult.Errors;
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
        /// Finds user in db by predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<User?> GetUserAsync(Expression<Func<User, bool>> predicate)
        {
            return await context.Users.FirstOrDefaultAsync(predicate);
        }

        public async Task<User?> FindUserByEmailOrUsernameAsync(EmailOrUsernameViewModel emailOrUsername)
        {
            User? user = await userManager.FindByEmailAsync(emailOrUsername.EmailOrUsername);
            if (user == null)
            {
                user = await userManager.FindByNameAsync(emailOrUsername.EmailOrUsername);
            }
            return user;
        }
    }
}

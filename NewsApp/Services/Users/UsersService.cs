using Microsoft.AspNetCore.Identity;
using NewsApp.Entities.Models;
using NewsApp.Entities.ViewModels;
using NewsApp.Repositories.Users;
using System.Linq.Expressions;
using System.Security.Principal;

namespace NewsApp.Services.Users
{
    public class UsersService : IUsersService
    {
        readonly IUserRepository userRepository;

        public UsersService(IUserRepository userRepository) 
        {
            this.userRepository = userRepository;
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
            return await userRepository.CreateUserAsync(userData);
        }


        /// <summary>
        /// Get user from db by identity
        /// </summary>
        /// <param name="context"></param>
        /// <param name="identity"></param>
        /// <returns></returns>
        public async Task<User?> GetUserAsync(IIdentity? identity)
        {
            return await userRepository.GetUserAsync(identity);
        }

        /// <summary>
        /// Finds user in db by predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<User?> GetUserAsync(Expression<Func<User, bool>> predicate)
        {
            return await userRepository.GetUserAsync(predicate);
        }

        /// <summary>
        /// Get user id by its identity
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public async Task<string?> GetUserIdAsync(IIdentity? identity)
        {
            return (await userRepository.GetUserAsync(identity)).Id;
        }
    }
}

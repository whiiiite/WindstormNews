﻿using Microsoft.AspNetCore.Identity;
using NewsApp.Entities.Models;
using NewsApp.Entities.ViewModels;
using System.Linq.Expressions;
using System.Security.Principal;

namespace NewsApp.Repositories.Users
{
    public interface IUserRepository
    {

        /// <summary>
        /// Creates user in db
        /// </summary>
        /// <param name="context"></param>
        /// <param name="userManager"></param>
        /// <param name="userData"></param>
        /// <returns></returns>
        public Task<IEnumerable<IdentityError>> CreateUserAsync(SignUpViewModel userData);
        /// <summary>
        /// Get user from db by identity
        /// </summary>
        /// <param name="context"></param>
        /// <param name="identity"></param>
        /// <returns></returns>
        public Task<User?> GetUserAsync(IIdentity? identity);
        /// <summary>
        /// Finds user in db by predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public Task<User?> GetUserAsync(Expression<Func<User, bool>> predicate);
        public Task<User?> FindUserByEmailOrUsernameAsync(EmailOrUsernameViewModel emailOrUsername);
    }
}

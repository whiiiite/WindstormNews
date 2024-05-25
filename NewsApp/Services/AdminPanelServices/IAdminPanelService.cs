using NewsApp.Entities.ViewModels;
using NewsApp.Shared;
using System.Security.Principal;

namespace NewsApp.Services.AdminPanelServices
{
    public interface IAdminPanelService
    {
        /// <summary>
        /// Adds user to role
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Result of operation and its info</returns>
        public Task<OperationResult> AddUserToRoleAsync(UserToRoleViewModel model);
        /// <summary>
        /// Removes user from role
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Result of operation and its info</returns>
        public Task<OperationResult> RemoveUserFromRoleAsync(UserToRoleViewModel model);
        /// <summary>
        /// Deletes user by email or username
        /// </summary>
        /// <param name="emailOrUsername"></param>
        /// <returns>Result of operation and its info</returns>
        public Task<OperationResult> DeleteUserAsync(EmailOrUsernameViewModel emailOrUsername, IIdentity currentUser);
    }
}

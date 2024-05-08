using NewsApp.Entities.ViewModels;
using NewsApp.Shared;

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
    }
}

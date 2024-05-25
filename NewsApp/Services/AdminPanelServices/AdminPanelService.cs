using Microsoft.AspNetCore.Identity;
using NewsApp.Entities.Models;
using NewsApp.Entities.ViewModels;
using NewsApp.Repositories.AdminPanelRepos;
using NewsApp.Repositories.EditorRoomRepositories;
using NewsApp.Repositories.Users;
using NewsApp.Shared;
using System.Security.Principal;

namespace NewsApp.Services.AdminPanelServices
{
    /// <summary>
    /// Service of admin panel that handles logic
    /// </summary>
    public class AdminPanelService : IAdminPanelService
    {
        private readonly IAdminPanelRepository adminPanelRepository;
        private readonly IUserRepository userRepository;
        public AdminPanelService(IAdminPanelRepository adminPanelRepository, IUserRepository userRepository)
        {
            this.adminPanelRepository = adminPanelRepository;
            this.userRepository = userRepository;
        }

        public async Task<OperationResult> AddUserToRoleAsync(UserToRoleViewModel model)
        {
            return await adminPanelRepository.AddUserToRoleAsync(model);
        }

        public async Task<OperationResult> RemoveUserFromRoleAsync(UserToRoleViewModel model)
        {
            return await adminPanelRepository.RemoveUserFromRoleAsync(model);
        }

        public async Task<OperationResult> DeleteUserAsync(EmailOrUsernameViewModel emailOrUsername, IIdentity currentUser)
        {
            User? user = await userRepository.GetUserAsync(currentUser);
            User? user2 = await userRepository.FindUserByEmailOrUsernameAsync(emailOrUsername);
            if(user2 != null && user == user2)
            {
                return OperationResult.IsNotSuccess("You can not delete yourself");
            }
            return await adminPanelRepository.DeleteUserAsync(emailOrUsername);
        }
    }
}

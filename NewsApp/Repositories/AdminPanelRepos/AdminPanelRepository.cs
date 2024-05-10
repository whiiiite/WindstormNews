using Microsoft.AspNetCore.Identity;
using NewsApp.Data;
using NewsApp.Entities.Models;
using NewsApp.Entities.ViewModels;
using NewsApp.Repositories.Users;
using NewsApp.Shared;

namespace NewsApp.Repositories.AdminPanelRepos
{
    public class AdminPanelRepository : IAdminPanelRepository
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;
        public AdminPanelRepository(IUserRepository userRepository, UserManager<User> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }

        public async Task<OperationResult> AddUserToRoleAsync(UserToRoleViewModel model)
        {
            return await _ChangeUserRoleStatus(model, isRemove: false);
        }

        public async Task<OperationResult> RemoveUserFromRoleAsync(UserToRoleViewModel model)
        {
            return await _ChangeUserRoleStatus(model, isRemove: true);
        }


        private async Task<OperationResult> _ChangeUserRoleStatus(UserToRoleViewModel model, bool isRemove = false)
        {
            User? user = await _userRepository.GetUserAsync(x => x.Email == model.UserEmail);

            if(user == null)
            {
                return OperationResult.IsNotSuccess("User does not exists");
            }

            if(isRemove)
            {
                await _userManager.RemoveFromRoleAsync(user, model.RoleName);
            }
            else
            {
                await _userManager.AddToRoleAsync(user, model.RoleName);
            }

            return OperationResult.SuccessInstance;
        }
    }
}

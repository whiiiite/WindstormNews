using NewsApp.Entities.ViewModels;
using NewsApp.Repositories.AdminPanelRepos;
using NewsApp.Shared;

namespace NewsApp.Services.AdminPanelServices
{
    public class AdminPanelService : IAdminPanelService
    {
        private readonly IAdminPanelRepository adminPanelRepository;
        public AdminPanelService(IAdminPanelRepository adminPanelRepository)
        {
            this.adminPanelRepository = adminPanelRepository;
        }

        public async Task<OperationResult> AddUserToRoleAsync(UserToRoleViewModel model)
        {
            return await adminPanelRepository.AddUserToRoleAsync(model);
        }

        public async Task<OperationResult> RemoveUserFromRoleAsync(UserToRoleViewModel model)
        {
            return await adminPanelRepository.RemoveUserFromRoleAsync(model);
        }
    }
}

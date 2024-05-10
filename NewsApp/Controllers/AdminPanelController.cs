using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NewsApp.Data;
using NewsApp.Entities.Models;
using NewsApp.Entities.ViewModels;
using NewsApp.Repositories.Users;
using NewsApp.Services.AdminPanelServices;
using NewsApp.Shared;
using System.Data;

namespace NewsApp.Controllers
{
    /// <summary>
    /// Controller for admin panel
    /// </summary>
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme,
        Roles = $"{RoleNames.Owner},{RoleNames.Admin}")]
    public class AdminPanelController : Controller
    {
        private readonly NewsAppContext _context;
        private readonly IAdminPanelService _adminPanelService;
        public AdminPanelController(NewsAppContext context, IAdminPanelService adminPanelService)
        {
            _context = context;
            _adminPanelService = adminPanelService;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult AddUserToRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddUserToRole([Bind("UserEmail, RoleName")]UserToRoleViewModel model)
        {
            OperationResult result = await _adminPanelService.AddUserToRoleAsync(model);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError("Isnotsuccess", result.Message);
                return View(model);
            }

            return View(nameof(Index));
        }

        [HttpGet]
        public IActionResult RemoveUserFromRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RemoveUserFromRole([Bind("UserEmail, RoleName")] UserToRoleViewModel model)
        {
            OperationResult result = await _adminPanelService.RemoveUserFromRoleAsync(model);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError("Isnotsuccess", result.Message);
                return View(model);
            }

            return View(nameof(Index));
        }
    }
}

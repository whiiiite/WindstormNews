﻿using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsApp.Data;
using NewsApp.Entities.ViewModels;
using NewsApp.Services.AdminPanelServices;
using NewsApp.Shared;

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


        public IActionResult CreateRole()
        {
            return View();
        }


        public IActionResult RemoveRole()
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

        [HttpGet]
        public IActionResult DeleteUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser([Bind("EmailOrUsername")] EmailOrUsernameViewModel emailOrUserNameVM)
        {
            OperationResult result = await _adminPanelService.DeleteUserAsync(emailOrUserNameVM, User.Identity);
            if (!result.IsSuccess)
            {
                return NotFound(result.Message);
            }
            return View(nameof(Index));
        }
    }
}

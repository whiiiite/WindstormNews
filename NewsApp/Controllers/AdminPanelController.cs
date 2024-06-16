using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly RoleManager<IdentityRole> _roleManager;
        public AdminPanelController(NewsAppContext context, IAdminPanelService adminPanelService, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _adminPanelService = adminPanelService;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult CreateRole()
        {
            return View(
                    new CreateRoleViewModel
                    {
                        RoleName = string.Empty,
                        Error = string.Empty
                    }
               );
        }


        [HttpPost]
        public async Task<IActionResult> CreateRole([FromForm]CreateRoleViewModel createRoleVM)
        {
            IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(createRoleVM.RoleName));
            if (!result.Succeeded)
            {
                return View(
                    new CreateRoleViewModel 
                    { 
                        RoleName = string.Empty, 
                        Error = result.Errors.FirstOrDefault()?.Description 
                    }
                );
            }

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public IActionResult RemoveRole()
        {
            return View(
                    new RemoveRoleViewModel
                    {
                        RoleName = string.Empty,
                        Error = string.Empty
                    }
               );
        }


        [HttpPost]
        public async Task<IActionResult> RemoveRole([FromForm] RemoveRoleViewModel removeRoleVM)
        {
            IdentityRole? role = await _context.Roles.FirstOrDefaultAsync(x => x.Name == removeRoleVM.RoleName);
            if(role == null)
            {
                return View(
                    new RemoveRoleViewModel
                    {
                        RoleName = string.Empty,
                        Error = $"Role with name {removeRoleVM.RoleName} does not exists"
                    }
               );
            }

            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
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

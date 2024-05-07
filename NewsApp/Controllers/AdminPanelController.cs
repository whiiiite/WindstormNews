using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NewsApp.Data;
using NewsApp.Entities.Models;
using NewsApp.Entities.ViewModels;
using NewsApp.Repositories.Users;
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
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;
        public AdminPanelController(NewsAppContext context, IUserRepository userRepository, UserManager<User> userManager)
        {
            _context = context;
            _userRepository = userRepository;
            _userManager = userManager;
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
            User? user = await _userRepository.GetUserAsync(x => x.Email == model.UserEmail);
            if (user == null)
            {
                ModelState.AddModelError("user_ne", "User does not exists");
                return View(model);
            }

            await _userManager.AddToRoleAsync(user, model.RoleName);

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
            User? user = await _userRepository.GetUserAsync(x => x.Email == model.UserEmail);
            if (user == null)
            {
                ModelState.AddModelError("user_ne", "User does not exists");
                return View(model);
            }

            await _userManager.RemoveFromRoleAsync(user, model.RoleName);

            return View(nameof(Index));
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using NewsApp.Entities.ViewModels;

namespace NewsApp.Controllers
{
    /// <summary>
    /// Controller for admin panel
    /// </summary>
    public class AdminPanelController : Controller
    {
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
        public IActionResult AddUserToRole([Bind("UserEmail, RoleId")]UserToRoleViewModel model)
        {
            return View();
        }
    }
}

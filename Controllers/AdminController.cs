using GamingForum.BusinessManagers.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GamingForum.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IAdminBusinessManager adminBusinessManager;
        public AdminController(IAdminBusinessManager adminBusinessManager)
        {
            this.adminBusinessManager = adminBusinessManager;
        }
        public async Task<IActionResult> Index()
        {
            return View(await adminBusinessManager.GetAdminDashboard(User));
        }

    }
}

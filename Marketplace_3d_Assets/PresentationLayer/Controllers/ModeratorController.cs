using Marketplace_3d_Assets.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace_3d_Assets.PresentationLayer.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ModeratorController : Controller
    {
        private readonly IModeratorService _moderatorService;

        public ModeratorController(IModeratorService moderatorService)
        {
            _moderatorService = moderatorService;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _moderatorService.GetAllUsersAsync();
            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> ToggleRole(Guid userId)
        {
            await _moderatorService.ToggleModeratorRoleAsync(userId);
            return RedirectToAction("Index");
        }
    }
}

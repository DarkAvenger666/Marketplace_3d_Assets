using Marketplace_3d_Assets.BusinessLogic.Interfaces;
using Marketplace_3d_Assets.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Marketplace_3d_Assets.PresentationLayer.Controllers
{
    public class ModerationController : Controller
    {
        private readonly IModerationService _moderationService;
        private readonly ApplicationContext _dbContext;
        public ModerationController(IModerationService moderationService, ApplicationContext applicationContext) 
        {
           
            _moderationService = moderationService;
            _dbContext = applicationContext;
        }
        [Authorize(Roles = "Moderator, Administrator")]
        public async Task<IActionResult> SendModerResult(Guid moderRequestId, string comment, bool result)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.User_Id == Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)));
            if (user == null) return NotFound();
            await _moderationService.SendModerResult(moderRequestId, user.User_Id, comment, result);
            return Ok();
        }
        /*[Authorize(Roles = "Moderator, Administrator")]
        public async Task<IActionResult> GetModerRequests()
        {
            var user = User.FindFirst(ClaimTypes.NameIdentifier, )
            await _moderationService.GetModerRequests();
            return View();
        }*/
    }
}

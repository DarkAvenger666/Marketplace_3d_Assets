using Marketplace_3d_Assets.BusinessLogic.Interfaces;
using Marketplace_3d_Assets.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Marketplace_3d_Assets.PresentationLayer.Controllers
{
    [Authorize(Roles = "Moderator, Administrator")]
    public class ModerationController : Controller
    {
        private readonly IModerationService _moderationService;
        private readonly ApplicationContext _dbContext;
        public ModerationController(IModerationService moderationService, ApplicationContext applicationContext) 
        {
           
            _moderationService = moderationService;
            _dbContext = applicationContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetMyModerRequests()
        {
            var moderatorId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var requests = await _moderationService.GetModerationRequestsAsync(moderatorId);
            return View("MyModerRequests", requests);
        }

        [HttpGet]
        public async Task<IActionResult> Review(Guid requestId)
        {
            var moderatorId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var model = await _moderationService.GetModerationDetailsAsync(requestId, moderatorId);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitModerationDecision(Guid requestId, string comment, string action)
        {
            var moderatorId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var request = await _dbContext.ModerationRequests.FirstOrDefaultAsync(mr => mr.Request_Id == requestId);
            if (request == null) return NotFound();
            if (request.User_Id != moderatorId) return Unauthorized();

            if (action == "ReturnedForRevision")
                await _moderationService.SendModerResult(requestId, moderatorId, comment, false);
            else if (action == "Published")
                await _moderationService.SendModerResult(requestId, moderatorId, comment, true);

            return RedirectToAction("MyModerationRequests");
        }
    }
}

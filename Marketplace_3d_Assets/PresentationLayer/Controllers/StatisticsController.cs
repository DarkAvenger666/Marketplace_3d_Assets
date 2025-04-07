using Marketplace_3d_Assets.BusinessLogic.Interfaces;
using Marketplace_3d_Assets.BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace_3d_Assets.PresentationLayer.Controllers
{
    public class StatisticsController : Controller
    {
        private readonly IStatisticsService _statisticsService;

        public StatisticsController(IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }
        [HttpGet]
        public async Task<IActionResult> PostStatistics()
        {
            var stats = await _statisticsService.GetPostStatisticsAsync();
            return View(stats);
        }

        [HttpGet]
        public async Task<IActionResult> AssetStatistics()
        {
            var stats = await _statisticsService.GetAssetStatisticsAsync();
            return View(stats);
        }
    }
}

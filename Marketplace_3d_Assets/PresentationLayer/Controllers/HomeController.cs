using Marketplace_3d_Assets.BusinessLogic.Interfaces;
using Marketplace_3d_Assets.BusinessLogic.Services;

//using Marketplace_3d_Assets.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Marketplace_3d_Assets.PresentationLayer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAssetService _assetService;

        public HomeController(ILogger<HomeController> logger, IAssetService assetService)
        {
            _logger = logger;
            _assetService = assetService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var assets = await _assetService.GetAssetsForMainPageAsync();
            return View(assets);
        }

        /*public IActionResult Privacy()
        {
            return View();
        }*/

        /*[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }*/
    }
}

using Marketplace_3d_Assets.BusinessLogic.Interfaces;
using Marketplace_3d_Assets.BusinessLogic.Services;
using Marketplace_3d_Assets.Data;
using Marketplace_3d_Assets.PresentationLayer.ViewModels.Filters;


//using Marketplace_3d_Assets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using static NuGet.Packaging.PackagingConstants;

namespace Marketplace_3d_Assets.PresentationLayer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAssetService _assetService;
        private readonly ApplicationContext _dbContext;
        private readonly IAssetTypeService _assetTypeService;

        public HomeController(ILogger<HomeController> logger, IAssetService assetService, 
                                ApplicationContext dbContext, IAssetTypeService assetTypeService)
        {
            _logger = logger;
            _assetService = assetService;
            _assetTypeService = assetTypeService;
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] AssetFilterModel filters)
        {
            var (assets, totalCount) = await _assetService.GetAssetsForMainPageAsync(filters);
            ViewBag.TotalPages = (int)Math.Ceiling(totalCount / (double)filters.PageSize);
            ViewBag.CurrentPage = filters.Page;
            var assetTypes = await _assetTypeService.GetAllAsync();
            ViewBag.AssetTypes = new SelectList(assetTypes, "Type_Id", "Name");
            ViewBag.Types = await _dbContext.AssetTypes.ToListAsync();

            return View(assets);
        }
    }
}

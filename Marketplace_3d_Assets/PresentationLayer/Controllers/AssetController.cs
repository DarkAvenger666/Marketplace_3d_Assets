﻿using Marketplace_3d_Assets.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Marketplace_3d_Assets.PresentationLayer.ViewModels;
using System.Security.Claims;

namespace Marketplace_3d_Assets.PresentationLayer.Controllers
{
    public class AssetController : Controller
    {
        private readonly IAssetService _assetService;
        private readonly IAssetTypeService _assetTypeService;
        public AssetController(IAssetService assetService, IAssetTypeService assetTypeService)
        {
            _assetService = assetService;
            _assetTypeService = assetTypeService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> UploadAsset()
        {
            var assetTypes = await _assetTypeService.GetAllAsync();
            ViewBag.AssetTypes = new SelectList(assetTypes, "Type_Id", "Name");
            return View();
        }

        /*[Authorize]
        [HttpGet]
        public IActionResult EditAssetDarft(Guid assetId)
        {
            var asset = _assetService.GetAssetFroEdit(assetId);
            return View("UploadAsset", );
        }*/

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SaveToDraft([FromForm] AssetUploadViewModel model)
        {
            /*Console.WriteLine(model.AssetId);*/
            var savedDraftId = await _assetService.SaveAssetToDarft(model);
            //return CreatedAtAction(nameof(GetById), new { id = savedModelId }, createdModel);

            TempData["SuccessMessage"] = $"Ассет с id - {savedDraftId} успешно сохранён в черновик!";
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public async Task<IActionResult> MyDrafts()
        {
            var profileId = Guid.Parse(User.FindFirstValue("ProfileId"));
            Console.WriteLine(profileId);
            var drafts = await _assetService.GetUserDraftsAsync(profileId);
            return View(drafts);
        }

        [Authorize]
        public async Task<IActionResult> SendToModeration(Guid assetId)
        {
            await _assetService.SendAssetToModeration(assetId);

            TempData["SuccessMessage"] = $"Ассет с id - {assetId} успешно отправлен на модерацию";
            return RedirectToAction("MyDrafts", "Asset");
        }

        [HttpGet]
        public async Task<IActionResult> AssetDetails([FromRoute(Name = "id")] Guid assetId)
        {
            var assetDetails = await _assetService.GetAssetDetailsAsync(assetId);
            return View(assetDetails);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SetOrRemoveForSale(Guid assetId, bool forSale)
        {
            var result = await _assetService.SetForSaleAsync(assetId, forSale);
            if (result) return Ok();
            return BadRequest();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ToggleLike(Guid assetId)
        {
            var userProfileId = Guid.Parse(User.FindFirst("ProfileId")?.Value);
            var isLiked = await _assetService.ToggleLikeAsync(assetId, userProfileId);
            var likesCount = _assetService.GetLikesCount(assetId);

            return Json(new { isLiked, likesCount });
        }
    }
    public class AssemblyMarker { }
}

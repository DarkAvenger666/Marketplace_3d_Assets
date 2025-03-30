using Marketplace_3d_Assets.BusinessLogic.Models_DTOs_;
using Marketplace_3d_Assets.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

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
        /*[HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var models = await _assetService.GetAllAsync();
            return Ok(models);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var model = await _assetService.GetByIdAsync(id);
            if (model == null) return NotFound();
            return Ok(model);
        }*/

        [HttpGet]
        public async Task<IActionResult> UploadAsset()
        {
            var assetTypes = await _assetTypeService.GetAllAsync();
            ViewBag.AssetTypes = new SelectList(assetTypes, "Type_Id", "Name");
            return View();
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SaveToDraft([FromForm] AssetDTO model)
        {
            var savedDraftId = await _assetService.SaveAssetToDarft(model);
            //return CreatedAtAction(nameof(GetById), new { id = savedModelId }, createdModel);
            return Ok($"Ассет с id - {savedDraftId} успешно сохранён");
        }

        /*[HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _assetService.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }*/
    }
    public class AssemblyMarker { }
}

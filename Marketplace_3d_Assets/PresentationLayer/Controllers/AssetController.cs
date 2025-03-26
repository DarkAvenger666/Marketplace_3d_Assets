using Marketplace_3d_Assets.BusinessLogic.Models_DTOs_;
using Marketplace_3d_Assets.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace_3d_Assets.PresentationLayer.Controllers
{
    public class AssetController : Controller
    {
        private readonly IAssetService _assetService;
       /* public AssetController(IAssetService assetService) 
        {
            _assetService = assetService;
        }
        [HttpGet]
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
        public IActionResult UploadAsset()
        {
            return View();
        }

        /*[HttpPost]
        public async Task<IActionResult> Create([FromBody] AssetDTO model)
        {
            var createdModel = await _assetService.CreateAsync(model);
            return CreatedAtAction(nameof(GetById), new { id = createdModel.Id }, createdModel);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _assetService.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }*/
    }
    public class AssemblyMarker { }
}

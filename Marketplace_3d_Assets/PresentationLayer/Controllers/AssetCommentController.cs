using Marketplace_3d_Assets.BusinessLogic.Interfaces;
using Marketplace_3d_Assets.PresentationLayer.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace_3d_Assets.PresentationLayer.Controllers
{
    public class AssetCommentController : Controller
    {
        private readonly IAssetCommentService _commentService;
        public AssetCommentController(IAssetCommentService assetCommentService) 
        {
            _commentService = assetCommentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetComments([FromQuery] Guid assetId)
        {
            var comments = await _commentService.GetCommentsAsync(assetId);
            return Ok(comments);
        }

        [HttpGet("AssetComment/GetByAssetPaged/{assetId}")]
        public async Task<IActionResult> GetCommentsByAssetPaged(Guid assetId, int page = 1, int pageSize = 10)
        {
            var comments = await _commentService.GetCommentsByAssetIdPaged(assetId, page, pageSize);
            return Ok(comments);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateComment([FromBody] CreateCommentDto comment)
        {
            if (string.IsNullOrWhiteSpace(comment.TextContent))
                return BadRequest("Comment cannot be empty.");

            var authorUserName = User.Identity?.Name ?? "Anonymous";
            var createdComment = await _commentService.CreateCommentAsync(comment, authorUserName);
            return Ok(createdComment);
        }

        [HttpDelete("{commentId}")]
        public async Task<IActionResult> DeleteComment(Guid commentId)
        {
            var result = await _commentService.DeleteCommentAsync(commentId);
            if (!result) return NotFound();
            return Ok();
        }
    }
}

using Marketplace_3d_Assets.BusinessLogic.Interfaces;
using Marketplace_3d_Assets.BusinessLogic.Services;
using Marketplace_3d_Assets.PresentationLayer.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Marketplace_3d_Assets.PresentationLayer.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var posts = await _postService.GetPostsForPostPage();
            return View(posts);
        }

        [Authorize]
        [HttpGet]
        public IActionResult PublishPost()
        {
            return View("PublishPost");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PublishPost([FromForm] PostUploadViewModel model)
        {
            var userProfileId = Guid.Parse(User.FindFirst("ProfileId")?.Value);
            if (userProfileId == Guid.Empty) return Unauthorized();
            var createdPostId = await _postService.CreatePost(model, userProfileId);
            return Ok($"Пост с id - {createdPostId} успешно опубликован");
        }

        [HttpGet]
        public async Task<IActionResult> PostDetails([FromRoute(Name = "id")] Guid postId)
        {
            var postDetails = await _postService.GetPostDetailsAsync(postId);
            return View(postDetails);
        }
    }
}

using Marketplace_3d_Assets.BusinessLogic.Interfaces;
using Marketplace_3d_Assets.Data;
using Marketplace_3d_Assets.DataAccess.Entities;
using Marketplace_3d_Assets.PresentationLayer.ViewModels;
using Microsoft.EntityFrameworkCore;
using NuGet.ContentModel;

namespace Marketplace_3d_Assets.BusinessLogic.Services
{
    public class PostService : IPostService
    {
        private readonly ApplicationContext _dbContext;
        private readonly IPostTagService _postTagService;
        public PostService(ApplicationContext dbContext, IPostTagService postTagService) 
        { 
            _dbContext = dbContext;
            _postTagService = postTagService;
        }

        public async Task<Guid> CreatePost(PostUploadViewModel postViewModel, Guid profileId)
        {
            var profile = _dbContext.UserProfiles.FirstOrDefault(p => p.Profile_Id == profileId);
            var postId = Guid.NewGuid();
            var post = new PostEntity
            {
                Post_Id = postId,
                Title = postViewModel.Title,
                Post_Text = postViewModel.Post_Text,
                Profile_Id = profileId,
                Profile = profile,
                Count_Of_Views = 0,
                Publication_Date = DateTime.UtcNow,
                Modified_Date = DateTime.UtcNow,
                
            };

            _dbContext.Posts.Add(post);
            await _dbContext.SaveChangesAsync();

            foreach (var tag in postViewModel.Tags)
            {
                await _postTagService.AttachTagToPost(tag, postId);
            }

            return postId;
        }

        public async Task<List<PostCardViewModel>> GetPostsForPostPage()
        {
            return await _dbContext.Posts
            .Include(p => p.Profile)
            .OrderByDescending(p => p.Publication_Date)
            .Select(p => new PostCardViewModel
            {
                Id = p.Post_Id,
                Title = p.Title,
                Post_Text = p.Post_Text,
                Author_Name = p.Profile.User_Name,
                Count_Of_Views = p.Count_Of_Views,
            })
            .ToListAsync();
        }
        public async Task<PostDetailsViewModel> GetPostDetailsAsync(Guid postId)
        {
            var post = await _dbContext.Posts.Include(p => p.Profile)
            .FirstOrDefaultAsync(p => p.Post_Id == postId);

            if (post == null) throw new Exception("Нет поста с таким id");

            return new PostDetailsViewModel
            {
                Id = post.Post_Id,
                Title = post.Title,
                Post_Text = post.Post_Text,
                Author_Name = post.Profile.User_Name,
                Count_Of_Views = post.Count_Of_Views,
                Publication_Date = post.Publication_Date,
                Modified_Date = post.Modified_Date,
                Tags = await _postTagService.GetPostTags(post.Post_Id)
            };
        }
    }
}

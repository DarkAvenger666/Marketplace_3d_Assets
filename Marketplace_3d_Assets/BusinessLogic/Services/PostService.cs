using Marketplace_3d_Assets.BusinessLogic.Interfaces;
using Marketplace_3d_Assets.Data;
using Marketplace_3d_Assets.DataAccess.Entities;
using Marketplace_3d_Assets.PresentationLayer.ViewModels;
using Marketplace_3d_Assets.PresentationLayer.ViewModels.Filters;
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
                Publication_Date = p.Publication_Date,
                Author_Name = p.Profile.User_Name,
                Count_Of_Views = p.Count_Of_Views,
            })
            .ToListAsync();
        }

        public async Task<(List<PostCardViewModel> Posts, int TotalCount)> GetFilteredPostsAsync(PostFilterViewModel filter)
        {
            var query = _dbContext.Posts
                .Include(p => p.Profile)
                .AsQueryable();

            // Поиск по запросу
            if (!string.IsNullOrWhiteSpace(filter.SearchQuery))
            {
                query = query.Where(p =>
                    p.Title.Contains(filter.SearchQuery) ||
                    p.Post_Text.Contains(filter.SearchQuery));
            }

            // Фильтрация по дате
            if (filter.StartDate.HasValue)
            {
                query = query.Where(p => p.Publication_Date >= filter.StartDate.Value);
            }

            if (filter.EndDate.HasValue)
            {
                query = query.Where(p => p.Publication_Date <= filter.EndDate.Value);
            }

            var totalCount = await query.CountAsync();

            var posts = await query
                .Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .Select(p => new PostCardViewModel
                {
                    Id = p.Post_Id,
                    Title = p.Title,
                    Post_Text = p.Post_Text,
                    Author_Name = p.Profile.User_Name,
                    Publication_Date = p.Publication_Date,
                    Count_Of_Views = p.Count_Of_Views
                })
                .ToListAsync();

            return (posts, totalCount);
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

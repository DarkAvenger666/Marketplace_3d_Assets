using Marketplace_3d_Assets.BusinessLogic.Interfaces;
using Marketplace_3d_Assets.Data;
using Marketplace_3d_Assets.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Marketplace_3d_Assets.BusinessLogic.Services
{
    public class PostTagService : IPostTagService
    {
        private readonly ApplicationContext _dbContext;
        private readonly ITagService _tagService;
        public PostTagService(ApplicationContext dbContext, ITagService tagService)
        {
            _dbContext = dbContext;
            _tagService = tagService;
        }
        public async Task AttachTagToPost(string tagName, Guid postId)
        {
            var postTag = new PostTagEntity()
            {
                Tag_Id = await GetOrCreatePostTagId(tagName),
                Post_Id = postId
            };
            await _dbContext.PostTags.AddAsync(postTag);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<bool> UnAttachTagFromPost(int TagId, Guid postId)
        {
            var postTag = _dbContext.PostTags.FirstOrDefault(pt => pt.Tag_Id == TagId && pt.Post_Id == postId);
            if (postTag != null)
            {
                _dbContext.PostTags.Remove(postTag);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }
        public async Task<List<string>> GetPostTags(Guid postId)
        {
            return await _dbContext.PostTags.Where(pt => pt.Post_Id == postId)
                .Include(pt => pt.Tag)
                .Select(pt => pt.Tag.Name)
                .ToListAsync();
        }
        public async Task<int> GetOrCreatePostTagId(string tagName)
        {
            return await _tagService.GetOrCreateTagId(tagName);
        }
    }
}

using Marketplace_3d_Assets.BusinessLogic.Interfaces;
using Marketplace_3d_Assets.Data;
using Marketplace_3d_Assets.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Marketplace_3d_Assets.BusinessLogic.Services
{
    public class TagService : ITagService
    {
        private readonly ApplicationContext _dbContext;
        public TagService(ApplicationContext dbContext) 
        {
            _dbContext = dbContext;
        }
        public async Task<int> GetOrCreateTagId(string tagName)
        {
            var tag = await _dbContext.Tags
                .Where(t => t.Name.ToLower() == tagName.ToLower())
                .FirstOrDefaultAsync();

            if (tag == null)
            {
                _dbContext.Tags.Add(new TagEntity() { Name = tagName.ToLower() });
                await _dbContext.SaveChangesAsync();
                tag = await _dbContext.Tags
                .Where(t => t.Name.ToLower() == tagName.ToLower())
                .FirstOrDefaultAsync();
            }

            return tag.Tag_Id;
        }
        public async Task<bool> DeleteTag(int tagId)
        {
            var tag = await _dbContext.Tags.FirstOrDefaultAsync(t => t.Tag_Id == tagId);
            if (tag != null)
            {
                _dbContext.Tags.Remove(tag);
                await _dbContext.SaveChangesAsync();
            }
            return false;
        }
    }
}

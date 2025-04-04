using Marketplace_3d_Assets.DataAccess.Entities;

namespace Marketplace_3d_Assets.BusinessLogic.Interfaces
{
    public interface IPostTagService
    {
        Task AttachTagToPost(string tagName, Guid postId);
        Task<bool> UnAttachTagFromPost(int TagId, Guid postId);
        Task<List<string>> GetPostTags(Guid postId);
        Task<int> GetOrCreatePostTagId(string tagName);
    }
}

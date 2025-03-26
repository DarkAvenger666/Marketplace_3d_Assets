namespace Marketplace_3d_Assets.BusinessLogic.Interfaces
{
    public interface ITagService
    {
        Task<int> GetOrCreateTagId(string tagName);
        Task<bool> DeleteTag(int tagId);
    }
}

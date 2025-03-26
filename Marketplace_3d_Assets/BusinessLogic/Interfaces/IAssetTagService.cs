using Marketplace_3d_Assets.DataAccess.Entities;

namespace Marketplace_3d_Assets.BusinessLogic.Interfaces
{
    public interface IAssetTagService
    {
        Task AttachTagToAsset(string tagName, Guid assetId);
        Task<bool> UnAttachTagFromAsset(int TagId, Guid assetId);
        Task<List<AssetTagEntity>> GetAssetTags(Guid assetId);
    }
}

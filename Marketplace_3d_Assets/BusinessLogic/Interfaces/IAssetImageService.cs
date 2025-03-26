namespace Marketplace_3d_Assets.BusinessLogic.Interfaces
{
    public interface IAssetImageService
    {
        Task SaveAssetImageAsync(IFormFile file, Guid assetId);
        Task<bool> DeleteAssetImageAsync(Guid id);
        Task<string> GetAssetImagePath(Guid id);
    }
}

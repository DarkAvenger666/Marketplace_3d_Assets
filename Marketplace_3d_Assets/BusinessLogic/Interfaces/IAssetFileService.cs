namespace Marketplace_3d_Assets.BusinessLogic.Interfaces
{
    public interface IAssetFileService
    {
        Task SaveAssetFileAsync(IFormFile file, Guid assetId);
        Task<int> GetOrCreateAssetFileType(string fileType);
        Task<bool> DeleteAssetFileAsync(Guid id);
        Task<string> GetAssetFilePath(Guid id);
    }
}

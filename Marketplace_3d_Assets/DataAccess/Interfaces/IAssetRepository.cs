using Marketplace_3d_Assets.DataAccess.Entities;
using Marketplace_3d_Assets.BusinessLogic.Models;
using Marketplace_3d_Assets.PresentationLayer.ViewModels;

namespace Marketplace_3d_Assets.DataAccess.Interfaces
{
    public interface IAssetRepository
    {
        Task<AssetEntity?> GetByIdAsync(Guid id);
        Task<IEnumerable<AssetEntity>> GetAllAsync();
        Task<IEnumerable<AssetCardDto>> GetAssetsForMainPageAsync();
        Task<AssetDetailedDto?> GetAssetDetailsAsync(Guid assetId);
        Task AddAsync(AssetEntity entity);
        Task UpdateAsync(AssetEntity entity);
        Task<bool> DeleteAsync(Guid id);
    }
}

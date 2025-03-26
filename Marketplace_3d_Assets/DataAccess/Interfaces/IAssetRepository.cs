using Marketplace_3d_Assets.DataAccess.Entities;

namespace Marketplace_3d_Assets.DataAccess.Interfaces
{
    public interface IAssetRepository
    {
        Task<AssetEntity?> GetByIdAsync(Guid id);
        Task<IEnumerable<AssetEntity>> GetAllAsync();
        Task AddAsync(AssetEntity entity);
        Task UpdateAsync(AssetEntity entity);
        Task<bool> DeleteAsync(Guid id);
    }
}

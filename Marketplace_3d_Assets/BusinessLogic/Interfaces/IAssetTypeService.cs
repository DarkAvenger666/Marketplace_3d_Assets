using Marketplace_3d_Assets.DataAccess.Entities;

namespace Marketplace_3d_Assets.BusinessLogic.Interfaces
{
    public interface IAssetTypeService
    {
        Task<List<AssetTypeEntity>> GetAllAsync();
    }
}

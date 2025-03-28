using Marketplace_3d_Assets.BusinessLogic.Models_DTOs_;
using Marketplace_3d_Assets.DataAccess.Entities;
using NuGet.Protocol.Core.Types;

namespace Marketplace_3d_Assets.BusinessLogic.Interfaces
{
    public interface IAssetService
    {
        Task<AssetEntity?> GetByIdAsync(Guid id);
        Task<Guid> SaveAssetToDarft(AssetDTO dtoModel);
    }
}

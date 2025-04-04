using Marketplace_3d_Assets.DataAccess.Entities;
using Marketplace_3d_Assets.PresentationLayer.ViewModels;
using NuGet.Protocol.Core.Types;

namespace Marketplace_3d_Assets.BusinessLogic.Interfaces
{
    public interface IAssetService
    {
        Task<Guid> SaveAssetToDarft(AssetUploadViewModel dtoModel);
        Task<List<AssetCardViewModel>> GetAssetsForMainPageAsync();
        Task<AssetDetailsViewModel> GetAssetDetailsAsync(Guid assetId);
        Task<bool> ToggleLikeAsync(Guid assetId, Guid userProfileId);
        Task<int> GetLikesCountAsync(Guid assetId);
    }
}

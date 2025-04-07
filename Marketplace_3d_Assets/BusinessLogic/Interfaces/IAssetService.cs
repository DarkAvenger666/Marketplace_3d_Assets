using Marketplace_3d_Assets.DataAccess.Entities;
using Marketplace_3d_Assets.PresentationLayer.ViewModels;
using Marketplace_3d_Assets.PresentationLayer.ViewModels.Filters;
using NuGet.Protocol.Core.Types;

namespace Marketplace_3d_Assets.BusinessLogic.Interfaces
{
    public interface IAssetService
    {
        Task<Guid> SaveAssetToDarft(AssetUploadViewModel dtoModel);
        /*Task<List<AssetCardViewModel>> GetAssetsForMainPageAsync();*/
        Task<(List<AssetCardViewModel> Assets, int TotalCount)> GetAssetsForMainPageAsync(AssetFilterModel filter);
        Task SendAssetToModeration(Guid assetId);
        Task<AssetDetailsViewModel> GetAssetDetailsAsync(Guid assetId);
        /*Task<AssetUploadViewModel> GetAssetDetailsAsync(*/
        Task<bool> ToggleLikeAsync(Guid assetId, Guid userProfileId);
        int GetLikesCount(Guid assetId);
        Task<bool> SetForSaleAsync(Guid assetId, bool forSale);
    }
}

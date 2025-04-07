using Marketplace_3d_Assets.PresentationLayer.ViewModels;

namespace Marketplace_3d_Assets.BusinessLogic.Interfaces
{
    public interface IModerationService
    {
        Task SendAssetToModeration(Guid assetId);
        Task SendModerResult(Guid moderRequestId, Guid moderUserId, string comment, bool result);
        Task<List<ModerationRequestViewModel>> GetModerationRequestsAsync(Guid moderatorId);
        Task<ModerationDetailsViewModel> GetModerationDetailsAsync(Guid requestId, Guid moderatorId);
    }
}

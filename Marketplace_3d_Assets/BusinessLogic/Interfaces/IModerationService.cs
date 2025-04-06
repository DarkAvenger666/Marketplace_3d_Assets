namespace Marketplace_3d_Assets.BusinessLogic.Interfaces
{
    public interface IModerationService
    {
        Task SendAssetToModeration(Guid assetId);
        Task SendModerResult(Guid moderRequestId, Guid moderUserId, string comment, bool result);
    }
}

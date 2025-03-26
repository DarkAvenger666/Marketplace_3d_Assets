namespace Marketplace_3d_Assets.BusinessLogic.Interfaces
{
    public interface IModerationService
    {
        Task SendAssetToModeration(Guid assetId);
    }
}

namespace Marketplace_3d_Assets.DataAccess.Interfaces
{
    public interface IModerationRepository
    {
        Task<Guid> GetUserWithMinModerationRequestsAsync();
    }
}

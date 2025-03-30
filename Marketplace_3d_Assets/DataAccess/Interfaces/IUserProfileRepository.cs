using Marketplace_3d_Assets.DataAccess.Entities;

namespace Marketplace_3d_Assets.DataAccess.Interfaces
{
    public interface IUserProfileRepository
    {
        Task<UserProfileEntity?> GetByIdAsync(Guid id);
        Task<IEnumerable<UserProfileEntity>> GetAllAsync();
        Task AddAsync(UserProfileEntity profile);
        Task UpdateAsync(UserProfileEntity profile);
        Task<bool> DeleteAsync(Guid id);
    }
}

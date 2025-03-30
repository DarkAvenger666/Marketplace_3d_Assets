using Marketplace_3d_Assets.Data;
using Marketplace_3d_Assets.DataAccess.Entities;
using Marketplace_3d_Assets.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Marketplace_3d_Assets.DataAccess.Repositories
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly ApplicationContext _context;

        public UserProfileRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<UserProfileEntity?> GetByIdAsync(Guid id)
        {
            return await _context.UserProfiles.FindAsync(id);
        }

        public async Task<IEnumerable<UserProfileEntity>> GetAllAsync()
        {
            return await _context.UserProfiles.ToListAsync();
        }

        public async Task AddAsync(UserProfileEntity profile)
        {
            profile.Creation_Date = DateTime.Now;
            profile.Modified_Date = DateTime.Now;
            await _context.UserProfiles.AddAsync(profile);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UserProfileEntity profile)
        {
            profile.Modified_Date = DateTime.Now;
            _context.UserProfiles.Update(profile);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var profile = await _context.UserProfiles.FindAsync(id);
            if (profile != null)
            {
                _context.UserProfiles.Remove(profile);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}

using Marketplace_3d_Assets.Data;
using Marketplace_3d_Assets.DataAccess.Entities;
using Marketplace_3d_Assets.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Marketplace_3d_Assets.DataAccess.Repositories
{
    public class AssetRepository : IAssetRepository
    {
        private readonly ApplicationContext _context;

        public AssetRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<AssetEntity?> GetByIdAsync(Guid id)
        {
            return await _context.Assets.FindAsync(id);
        }

        public async Task<IEnumerable<AssetEntity>> GetAllAsync()
        {
            return await _context.Assets.ToListAsync();
        }

        public async Task AddAsync(AssetEntity asset)
        {
            asset.UploadDate = DateTime.Now;
            asset.ModifiedDate = DateTime.Now;
            await _context.Assets.AddAsync(asset);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(AssetEntity asset)
        {
            asset.ModifiedDate = DateTime.Now;
            _context.Assets.Update(asset);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var asset = await _context.Assets.FindAsync(id);
            if (asset != null)
            {
                _context.Assets.Remove(asset);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}

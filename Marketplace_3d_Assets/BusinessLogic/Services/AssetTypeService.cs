using Marketplace_3d_Assets.BusinessLogic.Interfaces;
using Marketplace_3d_Assets.Data;
using Marketplace_3d_Assets.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Marketplace_3d_Assets.BusinessLogic.Services
{
    public class AssetTypeService : IAssetTypeService
    {
        private readonly ApplicationContext _dbContext;
        public AssetTypeService(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<AssetTypeEntity>> GetAllAsync()
        {
            return await _dbContext.AssetTypes.ToListAsync();
        }
    }
}

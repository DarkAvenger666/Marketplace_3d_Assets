using Marketplace_3d_Assets.Data;
using Marketplace_3d_Assets.DataAccess.Entities;
using Marketplace_3d_Assets.DataAccess.Interfaces;
using Marketplace_3d_Assets.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using NuGet.ContentModel;
using NuGet.Protocol.Core.Types;

namespace Marketplace_3d_Assets.BusinessLogic.Services
{
    public class ModerationService
    {
        private readonly ApplicationContext _dbContext;
        private readonly IModerationRepository _repository;
        public ModerationService(ApplicationContext dbContext, IModerationRepository repository) 
        {
            _dbContext = dbContext;
            _repository = repository;
        }
        public async Task SendAssetToModeration(Guid assetId)
        {
            var asset = _dbContext.Assets.FirstOrDefaultAsync(a => a.AssetId == assetId);
            if (asset == null) throw new Exception("Нет ассета с таким id");
            var moderationRequest = new ModerationRequestEntity()
            {
                RequestId = Guid.NewGuid(),
                AssetId = assetId,
                IsComplited = false,
                SendingDate = DateTime.Now,
                UserId = await _repository.GetUserWithMinModerationRequestsAsync()
            };
            await _dbContext.ModerationRequests.AddAsync(moderationRequest);
            await _dbContext.SaveChangesAsync();
        }

    }
}

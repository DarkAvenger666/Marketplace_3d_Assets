using Marketplace_3d_Assets.BusinessLogic.Interfaces;
using Marketplace_3d_Assets.Data;
using Marketplace_3d_Assets.DataAccess.Entities;
using Marketplace_3d_Assets.DataAccess.Interfaces;
using Marketplace_3d_Assets.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using NuGet.ContentModel;
using NuGet.Protocol.Core.Types;

namespace Marketplace_3d_Assets.BusinessLogic.Services
{
    public class ModerationService : IModerationService
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
            var asset = _dbContext.Assets.FirstOrDefaultAsync(a => a.Asset_Id == assetId);
            if (asset == null) throw new Exception("Нет ассета с таким id");
            var moderationRequest = new ModerationRequestEntity()
            {
                Request_Id = Guid.NewGuid(),
                Asset_Id = assetId,
                Is_Complited = false,
                Sending_Date = DateTime.Now,
                User_Id = await _repository.GetUserWithMinModerationRequestsAsync()
            };
            await _dbContext.ModerationRequests.AddAsync(moderationRequest);
            await _dbContext.SaveChangesAsync();
        }
        public async Task SendModerResult(Guid moderRequestId, Guid moderUserId, string comment, bool result)
        {
            var request = await _dbContext.ModerationRequests.FirstOrDefaultAsync(mr => mr.Request_Id == moderRequestId);
            if (request == null) throw new Exception("Нет запроса с таким Id");
            request.Completion_Date = DateTime.Now;
            request.Is_Complited = result;
            request.Comment = comment;

            var asset = await _dbContext.Assets.FirstOrDefaultAsync(a => a.Asset_Id == request.Asset_Id);
            if (asset == null) throw new Exception("Привязан неправильный ассет");
            if (result)
            {   
                asset.Upload_Date = DateTime.Now;
                asset.Status_Id = 3;
            }
            else
            {
                asset.Status_Id = 1;
            }
            _dbContext.ModerationRequests.Update(request);
            _dbContext.Assets.Update(asset);
            await _dbContext.SaveChangesAsync();
        }

    }
}

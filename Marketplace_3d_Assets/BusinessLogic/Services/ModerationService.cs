using Marketplace_3d_Assets.BusinessLogic.Interfaces;
using Marketplace_3d_Assets.Data;
using Marketplace_3d_Assets.DataAccess.Entities;
using Marketplace_3d_Assets.DataAccess.Interfaces;
using Marketplace_3d_Assets.DataAccess.Repositories;
using Marketplace_3d_Assets.PresentationLayer.ViewModels;
using Microsoft.EntityFrameworkCore;
using NuGet.ContentModel;
using NuGet.Protocol.Core.Types;
using System.Buffers;

namespace Marketplace_3d_Assets.BusinessLogic.Services
{
    public class ModerationService : IModerationService
    {
        private readonly ApplicationContext _dbContext;
        private readonly IModerationRepository _moderRepository;
        private readonly IAssetRepository _assetRepository;
        private readonly IAssetImageService _assetImageService;
        public ModerationService(ApplicationContext dbContext, IModerationRepository repository,
                                IAssetRepository assetRepository, IAssetImageService assetImageService) 
        {
            _dbContext = dbContext;
            _moderRepository = repository;
            _assetRepository = assetRepository;
            _assetImageService = assetImageService;
        }
        public async Task SendAssetToModeration(Guid assetId)
        {
            var asset = _dbContext.Assets.FirstOrDefaultAsync(a => a.Asset_Id == assetId);
            if (asset == null) throw new Exception("Нет ассета с таким id");
            var moderationRequest = new ModerationRequestEntity()
            {
                Request_Id = Guid.NewGuid(),
                Asset_Id = assetId,
                Status_Id = 1,
                Sending_Date = DateTime.Now,
                User_Id = await _moderRepository.GetUserWithMinModerationRequestsAsync()
            };
            await _dbContext.ModerationRequests.AddAsync(moderationRequest);
            await _dbContext.SaveChangesAsync();
        }
        public async Task SendModerResult(Guid moderRequestId, Guid moderUserId, string comment, bool result)
        {
            var request = await _dbContext.ModerationRequests.FirstOrDefaultAsync(mr => mr.Request_Id == moderRequestId);
            if (request == null) throw new Exception("Нет запроса с таким Id");

            if (request.User_Id != moderUserId) throw new Exception("Этот запрос привязан к другому модератору!");

            request.Completion_Date = DateTime.Now;
            request.Comment = comment;

            var asset = await _dbContext.Assets.FirstOrDefaultAsync(a => a.Asset_Id == request.Asset_Id);
            if (asset == null) throw new Exception("Привязан неправильный ассет");
            if (result)
            {
                request.Status_Id = 2;
                asset.Upload_Date = DateTime.Now;
                asset.Status_Id = 3;
            }
            else
            {
                request.Status_Id = 3;
                asset.Status_Id = 1;
            }
            _dbContext.ModerationRequests.Update(request);
            _dbContext.Assets.Update(asset);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<ModerationRequestViewModel>> GetModerationRequestsAsync(Guid moderatorId)
        {
            return await _dbContext.ModerationRequests
                .Where(r => r.User_Id == moderatorId && r.Status_Id == 1)
                .Include(r => r.Asset).ThenInclude(a => a.Type)
                .Include(r => r.Asset).ThenInclude(a => a.Profile)
                .Select(r => new ModerationRequestViewModel
                {
                    RequestId = r.Request_Id,
                    AssetTitle = r.Asset.Title,
                    AssetType = r.Asset.Type.Name,
                    AuthorUserName = r.Asset.Profile.User_Name,
                    SendingDateTime = r.Sending_Date
                })
                .ToListAsync();
        }
        public async Task<ModerationDetailsViewModel> GetModerationDetailsAsync(Guid requestId, Guid moderatorId)
        {
            var request = await _dbContext.ModerationRequests.FirstOrDefaultAsync(mr =>  mr.Request_Id == requestId);
            if (request == null) throw new ArgumentNullException(nameof(request));

            if (request.User_Id != moderatorId) throw new Exception("Этот запрос на модерацию не ваш!");

            var assetWithImagesId = (await _assetRepository.GetAssetDetailsAsync(request.Asset_Id));
            if (assetWithImagesId == null) throw new Exception("Ассет не найден");
            var moderDetailsWithImagesPath = new ModerationDetailsViewModel()
            {
                RequestId = requestId,
                Title = assetWithImagesId.Title,
                Description = assetWithImagesId.Description,
                Author_Name = assetWithImagesId.Author_Name,
                Type_Name = assetWithImagesId.Type_Name,
                Images = new List<string>(),
                Tags = assetWithImagesId.Tags,
                Price = assetWithImagesId.Price
            };
            for (int i = 0; i < assetWithImagesId.Images.Count(); i++)
            {
                moderDetailsWithImagesPath.Images.Add(
                    await _assetImageService.GetAssetImageRelationPath(assetWithImagesId.Images[i]));
            };

            return moderDetailsWithImagesPath;
        }
    }
}

using Marketplace_3d_Assets.BusinessLogic.Interfaces;
using Marketplace_3d_Assets.BusinessLogic.Models_DTOs_;
using Marketplace_3d_Assets.DataAccess.Entities;
using Marketplace_3d_Assets.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;

namespace Marketplace_3d_Assets.BusinessLogic.Services
{
    public class AssetService : IAssetService
    {
        private readonly IAssetRepository _repository;
        private readonly IAssetFileService _assetFileService;
        private readonly IAssetImageService _assetImageService;
        private readonly IAssetTagService _assetTagService;
        private readonly IModerationService _moderationService;
        public AssetService(IAssetRepository assetRepository, IAssetFileService assetFileService,
                            IAssetImageService assetImageService, IAssetTagService assetTagService,
                            IModerationService moderationService) 
        {
            _repository = assetRepository;
            _assetFileService = assetFileService;
            _assetImageService = assetImageService;
            _assetTagService = assetTagService;
            _moderationService = moderationService;
        }
        public async Task<IEnumerable<AssetEntity>> GetAllAsync() => await _repository.GetAllAsync();

        public async Task<AssetEntity?> GetByIdAsync(Guid id) => await _repository.GetByIdAsync(id);

        public async Task SaveAssetToDarft(AssetDTO dtoModel)
        {
            Guid assetId = Guid.NewGuid();
            var model = new AssetEntity()
            {
                AssetId = assetId,
                Title = dtoModel.Title,
                TypeId = dtoModel.TypeId,
                StatusId = 1,      //SavedInDraft Type
                CountOfCopiesSold = 0,
                CountOfLikes = 0,
                CountOfViews = 0,
                AssetDescription = dtoModel.AssetDescription,
                Price = dtoModel.Price,
                /*ProfileId ,*/
            };
            await _repository.AddAsync(model);

            foreach (var file in dtoModel.Files)
            {
                await _assetFileService.SaveAssetFileAsync(file, assetId);
            }
            foreach (var image in dtoModel.Images)
            {
                await _assetImageService.SaveAssetImageAsync(image, assetId);
            }
            foreach (var tag in dtoModel.Tags)
            {
                await _assetTagService.AttachTagToAsset(tag, assetId);
            }
        }

        public async Task SendAssetToModeration(Guid assetId)
        {
            var asset = await _repository.GetByIdAsync(assetId);
            if (asset == null) throw new Exception("Ассета с таким id не существует");
            switch(asset.StatusId)
            {
                case 1:
                    await _moderationService.SendAssetToModeration(assetId);
                    asset.StatusId = 2;
                    await _repository.UpdateAsync(asset);
                    break;
                case 2:
                    throw new Exception("Ассет уже находиться на модерации");
                case 3:
                    throw new Exception("Ассет уже опубликован");
                case 4:
                    throw new Exception("Ассет уже опубликован, но снят с продаж");
            }
        }

        public async Task<bool> DeleteAsync(Guid id) => await _repository.DeleteAsync(id);
    }
}

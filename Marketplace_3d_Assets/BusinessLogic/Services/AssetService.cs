using Marketplace_3d_Assets.BusinessLogic.Interfaces;
using Marketplace_3d_Assets.BusinessLogic.Models_DTOs_;
using Marketplace_3d_Assets.DataAccess.Entities;
using Marketplace_3d_Assets.DataAccess.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using System.Security.Claims;

namespace Marketplace_3d_Assets.BusinessLogic.Services
{
    public class AssetService : IAssetService
    {
        private readonly IAssetRepository _repository;
        private readonly IAssetFileService _assetFileService;
        private readonly IAssetImageService _assetImageService;
        private readonly IAssetTagService _assetTagService;
        private readonly IModerationService _moderationService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AssetService(IAssetRepository assetRepository, IAssetFileService assetFileService,
                            IAssetImageService assetImageService, IAssetTagService assetTagService,
                            IModerationService moderationService, IHttpContextAccessor httpContextAccessor) 
        {
            _repository = assetRepository;
            _assetFileService = assetFileService;
            _assetImageService = assetImageService;
            _assetTagService = assetTagService;
            _moderationService = moderationService;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IEnumerable<AssetEntity>> GetAllAsync() => await _repository.GetAllAsync();

        public async Task<AssetEntity?> GetByIdAsync(Guid id) => await _repository.GetByIdAsync(id);

        public async Task<Guid> SaveAssetToDarft(AssetDTO dtoModel)
        {
            Guid assetId = Guid.NewGuid();

            string test = _httpContextAccessor.HttpContext?.User.FindFirstValue("ProfileId");
            Console.WriteLine(test);

            var profileId = Guid.Parse(_httpContextAccessor.HttpContext?.User.FindFirstValue("ProfileId"));
            var model = new AssetEntity()
            {
                Asset_Id = assetId,
                Title = dtoModel.Title,
                Type_Id = dtoModel.TypeId,
                Status_Id = 1,      //SavedInDraft Type
                Count_Of_Copies_Sold = 0,
                Count_Of_Likes = 0,
                Count_Of_Views = 0,
                Asset_Description = dtoModel.AssetDescription,
                Price = dtoModel.Price,
                Profile_Id = profileId
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
            return assetId;
        }

        public async Task SendAssetToModeration(Guid assetId)
        {
            var asset = await _repository.GetByIdAsync(assetId);
            if (asset == null) throw new Exception("Ассета с таким id не существует");
            switch(asset.Status_Id)
            {
                case 1:
                    await _moderationService.SendAssetToModeration(assetId);
                    asset.Status_Id = 2;
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

using Marketplace_3d_Assets.BusinessLogic.Interfaces;
using Marketplace_3d_Assets.Data;
using Marketplace_3d_Assets.DataAccess.Entities;
using Marketplace_3d_Assets.DataAccess.Interfaces;
using Marketplace_3d_Assets.PresentationLayer.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Marketplace_3d_Assets.BusinessLogic.Services
{
    public class AssetService : IAssetService
    {
        private readonly ApplicationContext _dbContext;
        private readonly IAssetRepository _repository;
        private readonly IAssetFileService _assetFileService;
        private readonly IAssetImageService _assetImageService;
        private readonly IAssetTagService _assetTagService;
        private readonly IModerationService _moderationService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AssetService(IAssetRepository assetRepository, IAssetFileService assetFileService,
                            IAssetImageService assetImageService, IAssetTagService assetTagService,
                            IModerationService moderationService, IHttpContextAccessor httpContextAccessor,
                            ApplicationContext dbContext) 
        {
            _repository = assetRepository;
            _assetFileService = assetFileService;
            _assetImageService = assetImageService;
            _assetTagService = assetTagService;
            _moderationService = moderationService;
            _httpContextAccessor = httpContextAccessor;
            _dbContext = dbContext;
        }

        public async Task<Guid> SaveAssetToDarft(AssetUploadViewModel dtoModel)
        {
            var profileId = Guid.Parse(_httpContextAccessor.HttpContext?.User.FindFirstValue("ProfileId"));

            var existingAsset = await _repository.GetByIdAsync(
                a.Title == dtoModel.Title && a.Profile_Id == profileId);

            Guid assetId = Guid.NewGuid();

            
            var model = new AssetEntity()
            {
                Asset_Id = assetId,
                Title = dtoModel.Title,
                Type_Id = dtoModel.TypeId,
                Status_Id = 1,      //SavedInDraft Type
                Count_Of_Copies_Sold = 0,
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

        public async Task<List<AssetCardViewModel>> GetAssetsForMainPageAsync()
        {
            var assetsWithImageId = (await _repository.GetAssetsForMainPageAsync()).ToList();
            var assetsWithImagePath = new List<AssetCardViewModel>();
            for (int i = 0; i < assetsWithImageId.Count(); i++)
            {
                var assetWithImgPath = new AssetCardViewModel()
                {
                    Id = assetsWithImageId[i].Id,
                    Title = assetsWithImageId[i].Title,
                    Author_Name = assetsWithImageId[i].Author_Name,
                    Type_Name = assetsWithImageId[i].Type_Name,
                    Count_Of_Copies_Sold = assetsWithImageId[i].Count_Of_Copies_Sold,
                    Asset_Image = await _assetImageService.GetAssetImageRelationPath(assetsWithImageId[i].Asset_Image_Id),
                    Price = assetsWithImageId[i].Price,
                    Count_Of_Comments = assetsWithImageId[i].Count_of_Comments,
                    Count_Of_Views = assetsWithImageId[i].Count_Of_Views,
                    Count_Of_Likes = await GetLikesCountAsync(assetsWithImageId[i].Id)
                };
                assetsWithImagePath.Add(assetWithImgPath);
            }
            return assetsWithImagePath;
        }
        public async Task<AssetDetailsViewModel> GetAssetDetailsAsync(Guid assetId)
        {
            var assetWithImagesId = (await _repository.GetAssetDetailsAsync(assetId));
            if (assetWithImagesId == null) throw new Exception("Ассет не найден");
            var assetWithImagesPath = new AssetDetailsViewModel()
            {
                Id = assetWithImagesId.Id,
                Title = assetWithImagesId.Title,
                Description = assetWithImagesId.Description,
                Author_Name = assetWithImagesId.Author_Name,
                Type_Name = assetWithImagesId.Type_Name,
                Upload_Date = assetWithImagesId.Upload_Date,
                Modified_Date = assetWithImagesId.Modified_Date,
                Count_Of_Copies_Sold = assetWithImagesId.Count_Of_Copies_Sold,
                Images = new List<string>(),
                Tags = assetWithImagesId.Tags,
                Price = assetWithImagesId.Price,
                Count_Of_Comments = assetWithImagesId.Count_Of_Comments,
                Count_Of_Views = assetWithImagesId.Count_Of_Views,
                Count_Of_Likes = await GetLikesCountAsync(assetWithImagesId.Id)
            };
            for (int i = 0; i < assetWithImagesId.Images.Count(); i++)
            {
                assetWithImagesPath.Images.Add(
                    await _assetImageService.GetAssetImageRelationPath(assetWithImagesId.Images[i]));
            };

            return assetWithImagesPath;
        }

        public async Task<bool> ToggleLikeAsync(Guid assetId, Guid userProfileId)
        {
            var like = await _dbContext.AssetLikes
                .FirstOrDefaultAsync(l => l.Asset_Id == assetId && l.Profile_Id == userProfileId);

            if (like != null)
            {
                _dbContext.AssetLikes.Remove(like);
            }
            else
            {
                _dbContext.AssetLikes.Add(new AssetLikeEntity {Asset_Id = assetId, Profile_Id = userProfileId });
            }

            await _dbContext.SaveChangesAsync();
            return like == null; // Вернёт true, если поставили лайк, false если убрали
        }

        public async Task<int> GetLikesCountAsync(Guid assetId)
        {
            return await _dbContext.AssetLikes.CountAsync(l => l.Asset_Id == assetId);
        }

        public async Task<bool> DeleteAsync(Guid id) => await _repository.DeleteAsync(id);
    }
}

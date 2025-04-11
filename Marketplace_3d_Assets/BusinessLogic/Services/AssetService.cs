using Marketplace_3d_Assets.BusinessLogic.Interfaces;
using Marketplace_3d_Assets.Data;
using Marketplace_3d_Assets.DataAccess.Entities;
using Marketplace_3d_Assets.DataAccess.Interfaces;
using Marketplace_3d_Assets.PresentationLayer.ViewModels;
using Marketplace_3d_Assets.PresentationLayer.ViewModels.Filters;
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

            /*var existingAsset = await _repository.GetByIdAsync(
                a.Title == dtoModel.Title && a.Profile_Id == profileId);*/
            var assetExist = await _dbContext.Assets.AnyAsync(a => a.Asset_Id == dtoModel.AssetId);

            Guid assetId;
            if (!assetExist) 
            {
                assetId = Guid.NewGuid();
            }
            else
            {
                assetId = dtoModel.AssetId;
            }

            
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
                Profile_Id = profileId,
                
            };

            if (!assetExist)
            {
                await _repository.AddAsync(model);
            }
            else
            {
                await _repository.UpdateAsync(model);
            }

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

        public async Task<List<AssetPreviewViewModel>> GetUserDraftsAsync(Guid userId)
        {
            return await _dbContext.Assets
                .Where(a => a.Profile_Id == userId && a.Status_Id == 1)
                .Select(a => new AssetPreviewViewModel
                {
                    AssetId = a.Asset_Id,
                    Title = a.Title,
                    Price = a.Price,
                    TypeName = a.Type.Name,
                })
                .ToListAsync();
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

        public async Task<(List<AssetCardViewModel> Assets, int TotalCount)> GetAssetsForMainPageAsync(AssetFilterModel filter)
        {
            var query = _dbContext.Assets
                .Include(a => a.Profile)
                .Include(a => a.Asset_Images)
                .Include(a => a.Asset_Tags)
                .Where(a => a.Status_Id == 3)
                .AsQueryable();

            if (filter.TypeId.HasValue)
                query = query.Where(a => a.Type_Id == filter.TypeId);

            if (filter.MinPrice.HasValue)
                query = query.Where(a => a.Price >= filter.MinPrice);

            if (filter.MaxPrice.HasValue)
                query = query.Where(a => a.Price <= filter.MaxPrice);

            if (!string.IsNullOrWhiteSpace(filter.SearchQuery))
            {
                var search = filter.SearchQuery.ToLower();
                query = query.Where(a => a.Title.ToLower().Contains(search) || a.Asset_Description.ToLower().Contains(search));
            }

            query = filter.SortBy switch
            {
                "price" => filter.SortDescending ? query.OrderByDescending(a => a.Price) : query.OrderBy(a => a.Price),
                "likes" => filter.SortDescending ? query.OrderByDescending(a => a.Asset_Likes.Count) : query.OrderBy(a => a.Asset_Likes.Count),
                "comments" => filter.SortDescending ? query.OrderByDescending(a => a.Asset_Comments.Count()) : query.OrderBy(a => a.Asset_Comments.Count()),
                _ => query.OrderByDescending(a => a.Upload_Date)
            };

            var totalCount = await query.CountAsync();

            var items = await query
                .Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .Select(a => new AssetCardViewModel
                {
                    Id = a.Asset_Id,
                    Title = a.Title,
                    Author_Name = a.Profile.User_Name,
                    Asset_Image = a.Asset_Images.Select(i => i.Asset_Image_Id).FirstOrDefault().ToString(),
                    Count_Of_Copies_Sold = a.Count_Of_Copies_Sold,
                    Count_Of_Comments = a.Asset_Comments.Count(),
                    Count_Of_Views = a.Count_Of_Views,
                    Count_Of_Likes = a.Asset_Likes.Count(),
                    Price = a.Price
                }).ToListAsync();

            for (int i = 0; i < items.Count; i++)
            {
                items[i].Asset_Image = await _assetImageService.GetAssetImageRelationPath(Guid.Parse(items[i].Asset_Image));
            }

            return (items, totalCount);
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
                Count_Of_Likes = GetLikesCount(assetWithImagesId.Id)
            };
            for (int i = 0; i < assetWithImagesId.Images.Count(); i++)
            {
                assetWithImagesPath.Images.Add(
                    await _assetImageService.GetAssetImageRelationPath(assetWithImagesId.Images[i]));
            };

            return assetWithImagesPath;
        }

        public async Task<bool> SetForSaleAsync(Guid assetId, bool forSale)
        {
            var asset = await _dbContext.Assets.FindAsync(assetId);
            if (asset == null)
                return false;

            if (asset.Status_Id == 1 || asset.Status_Id == 2) return false;

            asset.Status_Id = forSale ? 3 : 4; // 3 — на продаже, иначе снять (4)

            await _dbContext.SaveChangesAsync();
            return true;
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

        public int GetLikesCount(Guid assetId)
        {
            return _dbContext.AssetLikes.Count(l => l.Asset_Id == assetId);
        }

        public async Task<bool> DeleteAsync(Guid id) => await _repository.DeleteAsync(id);
    }
}

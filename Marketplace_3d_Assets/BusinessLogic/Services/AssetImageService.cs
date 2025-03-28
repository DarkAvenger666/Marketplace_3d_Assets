using Marketplace_3d_Assets.BusinessLogic.Interfaces;
using Marketplace_3d_Assets.Data;
using Marketplace_3d_Assets.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Marketplace_3d_Assets.BusinessLogic.Services
{
    public class AssetImageService : IAssetImageService
    {
        private readonly IFileService _fileService;
        private readonly ApplicationContext _dbContext;
        public AssetImageService(IFileService fileService, ApplicationContext dbContext)
        {
            _fileService = fileService;
            _dbContext = dbContext;
        }
        public async Task SaveAssetImageAsync(IFormFile image, Guid assetId)
        {
            string folderName = Path.Combine("images", "assetImages");
            string fileName = await _fileService.SaveFileAsync(image, folderName);

            var assetImage = new AssetImageEntity
            {
                Asset_Image_Id = Guid.NewGuid(),
                Asset_Id = assetId,
                Name = fileName
            };
            _dbContext.AssetImages.Add(assetImage);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<bool> DeleteAssetImageAsync(Guid id)
        {
            var assetImage = await _dbContext.AssetImages.FirstOrDefaultAsync(i => i.Asset_Image_Id == id);
            if (assetImage != null)
            {
                string filePath = _fileService.GetFilePath(Path.Combine("images", "assetImages"), assetImage.Name);
                _dbContext.AssetImages.Remove(assetImage);
                await _dbContext.SaveChangesAsync();

                bool delRes = await _fileService.DeleteFileAsync(filePath);
                if (delRes) return true;
                return false;
            }
            return false;
        }
        public async Task<string> GetAssetImagePath(Guid id)
        {
            var assetImage = await _dbContext.AssetImages.FirstOrDefaultAsync(i => i.Asset_Image_Id == id);
            if (assetImage != null)
            {
                return _fileService.GetFilePath(Path.Combine("images", "assetImages"), assetImage.Name);
            }
            throw new Exception("Изображения с таким id не существует");
        }
    }
}

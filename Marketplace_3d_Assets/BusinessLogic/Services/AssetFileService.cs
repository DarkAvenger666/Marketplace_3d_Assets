using Marketplace_3d_Assets.BusinessLogic.Interfaces;
using Marketplace_3d_Assets.Data;
using Marketplace_3d_Assets.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using NuGet.ContentModel;

namespace Marketplace_3d_Assets.BusinessLogic.Services
{
    public class AssetFileService : IAssetFileService
    {
        private readonly IFileService _fileService;
        private readonly ApplicationContext _dbContext;
        public AssetFileService(IFileService fileService, ApplicationContext dbContext) 
        {
            _fileService = fileService;
            _dbContext = dbContext;
        }
        public async Task SaveAssetFileAsync(IFormFile file, Guid assetId)
        {
            string fileName = await _fileService.SaveFileAsync(file, "assetFiles");

            var assetFile = new AssetFileEntity
            {
                Asset_File_Id = Guid.NewGuid(),
                Asset_Id = assetId,
                File_Type_Id = await GetOrCreateAssetFileType(Path.GetExtension(fileName)),
                File_Name = fileName
            };
            _dbContext.AssetFiles.Add(assetFile);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<int> GetOrCreateAssetFileType(string fileType)
        {
            var assetFileType = await _dbContext.FileTypes
                .Where(t => t.Name.ToLower() == fileType.ToLower())
                .FirstOrDefaultAsync();

            if (assetFileType == null)
            {
                _dbContext.FileTypes.Add(new FileTypeEntity() { Name = fileType.ToLower() });
                await _dbContext.SaveChangesAsync();
                assetFileType = await _dbContext.FileTypes
                .Where(t => t.Name.ToLower() == fileType.ToLower())
                .FirstOrDefaultAsync();
            }

            return assetFileType.File_Type_Id;
        }
        public async Task<bool> DeleteAssetFileAsync(Guid id)
        {
            var assetFile = await _dbContext.AssetFiles.FirstOrDefaultAsync(f => f.Asset_File_Id == id);
            if (assetFile != null)
            {
                string filePath = _fileService.GetFilePath("assetFiles", assetFile.File_Name);
                _dbContext.AssetFiles.Remove(assetFile);
                await _dbContext.SaveChangesAsync();

                bool delRes = await _fileService.DeleteFileAsync(filePath);
                if (delRes) return true;
                return false;
            }
            return false;
        }
        public async Task<string> GetAssetFilePath(Guid id)
        {
            var assetFile = await _dbContext.AssetFiles.FirstOrDefaultAsync(f => f.Asset_File_Id == id);
            if (assetFile != null)
            {
                return _fileService.GetFilePath("assetFiles", assetFile.File_Name);
            }
            throw new Exception("Файла с таким id не существует");
        }
    }
}

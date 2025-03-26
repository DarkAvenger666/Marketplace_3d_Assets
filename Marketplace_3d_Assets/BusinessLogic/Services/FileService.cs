using Marketplace_3d_Assets.BusinessLogic.Interfaces;

namespace Marketplace_3d_Assets.BusinessLogic.Services
{
    public class FileService : IFileService
    {
        private readonly string _basePath = Path.Combine(Directory.GetCurrentDirectory(), "PresentationLayer", "wwwroot");

        public async Task<string> SaveFileAsync(IFormFile file, string folderName)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("Файл пустой или не существует");

            string fileExtension = Path.GetExtension(file.FileName).ToLower();
            string folderPath = Path.Combine(_basePath, folderName, fileExtension);
            Directory.CreateDirectory(folderPath);

            Guid fileGuid = Guid.NewGuid();
            string fileName = $"{fileGuid}_{file.FileName}";
            string filePath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return fileName;
        }

        public async Task<bool> DeleteFileAsync(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return true;
            }
            return false;
        }
        public string GetFilePath(string folderName, string fileName)
        {
            return Path.Combine(_basePath, folderName, Path.GetExtension(fileName).ToLower(), fileName);
        }

    }
}

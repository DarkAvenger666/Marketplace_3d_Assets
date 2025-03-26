namespace Marketplace_3d_Assets.BusinessLogic.Interfaces
{
    public interface IFileService
    {
        Task<string> SaveFileAsync(IFormFile file, string folderName);
        Task<bool> DeleteFileAsync(string filePath);
        string GetFilePath(string folderName, string fileName);
    }
}

using Marketplace_3d_Assets.Data;
using Marketplace_3d_Assets.DataAccess.Entities;
using Marketplace_3d_Assets.DataAccess.Interfaces;
using Marketplace_3d_Assets.BusinessLogic.Models;
using Microsoft.EntityFrameworkCore;
using Dapper;
using Marketplace_3d_Assets.PresentationLayer.ViewModels;
using NuGet.ContentModel;

namespace Marketplace_3d_Assets.DataAccess.Repositories
{
    public class AssetRepository : IAssetRepository
    {
        private readonly ApplicationContext _context;

        public AssetRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<AssetEntity?> GetByIdAsync(Guid id)
        {
            return await _context.Assets.FindAsync(id);
        }

        public async Task<IEnumerable<AssetEntity>> GetAllAsync()
        {
            return await _context.Assets.ToListAsync();
        }

        public async Task AddAsync(AssetEntity asset)
        {
            asset.Upload_Date = DateTime.Now;
            asset.Modified_Date = DateTime.Now;
            await _context.Assets.AddAsync(asset);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(AssetEntity asset)
        {
            asset.Modified_Date = DateTime.Now;
            _context.Assets.Update(asset);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var asset = await _context.Assets.FindAsync(id);
            if (asset != null)
            {
                _context.Assets.Remove(asset);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<IEnumerable<AssetCardDto>> GetAssetsForMainPageAsync()
        {
            //sortingOrder = 
            string sql = @"
            SELECT 
                a.Asset_Id AS Id, 
                a.Title,
                p.User_Name as Author_Name,
                t.Name as Type_Name,
                a.Count_Of_Copies_Sold,
                i.Asset_Image_Id,
                a.Price,
                a.Count_Of_Views,
                COALESCE(c.CommentCount, 0) AS Count_of_Comments
            FROM asset a
            LEFT JOIN (
                SELECT Asset_Id, Asset_Image_Id 
                FROM (
                    SELECT Asset_Id, Asset_Image_Id, ROW_NUMBER() OVER (PARTITION BY Asset_Id ORDER BY Name) AS rn
                    FROM Asset_Image
                ) AS ranked
                WHERE rn = 1
            ) i ON i.Asset_Id = a.Asset_Id
            LEFT JOIN user_profile p ON p.Profile_Id = a.Profile_Id
            LEFT JOIN asset_type t ON t.Type_Id = a.Type_Id
            LEFT JOIN (
                SELECT Asset_Id, COUNT(*) AS CommentCount 
                FROM Asset_Comment 
                GROUP BY Asset_Id
            ) c ON c.Asset_Id = a.Asset_Id
            ORDER BY a.Price;
            ";
            
            return await _context.Database.GetDbConnection().QueryAsync<AssetCardDto>(sql);
        }

        public async Task<AssetDetailedDto?> GetAssetDetailsAsync(Guid assetId)
        {
            Console.WriteLine(assetId);
            return await _context.Assets
                .Include(a => a.Asset_Images)
                .Include(a => a.Asset_Tags)
                .Include(a => a.Profile)
                .Include(a => a.Type)
                .Include(a => a.Asset_Comments)
                .Where(a => a.Asset_Id == assetId)
                .Select(a => new AssetDetailedDto
                {
                    Id = a.Asset_Id,
                    Title = a.Title,
                    Description = a.Asset_Description,
                    Type_Name = a.Type.Name,
                    Author_Name = a.Profile.User_Name,
                    Price = a.Price,
                    Upload_Date = a.Upload_Date,
                    Modified_Date = a.Modified_Date,
                    Count_Of_Copies_Sold = a.Count_Of_Copies_Sold,
                    Count_Of_Views = a.Count_Of_Views,
                    Count_Of_Comments = a.Asset_Comments.Count(),
                    Images = a.Asset_Images.Select(i => i.Asset_Image_Id).ToList(),
                    Tags = a.Asset_Tags.Select(t => t.Tag.Name).ToList()
                })
                .FirstOrDefaultAsync();
        }
    }
}

﻿using Marketplace_3d_Assets.BusinessLogic.Interfaces;
using Marketplace_3d_Assets.Data;
using Marketplace_3d_Assets.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using NuGet.ContentModel;

namespace Marketplace_3d_Assets.BusinessLogic.Services
{
    public class AssetTagService : IAssetTagService
    {
        private readonly ApplicationContext _dbContext;
        private readonly ITagService _tagService;
        public AssetTagService(ApplicationContext dbContext, ITagService tagService)
        {
            _dbContext = dbContext;
            _tagService = tagService;
        }
        public async Task AttachTagToAsset(string tagName, Guid assetId)
        {
            var assetTag = new AssetTagEntity()
            {
                Tag_Id = await GetOrCreateAssetTagId(tagName),
                Asset_Id = assetId
            };
            await _dbContext.AssetTags.AddAsync(assetTag);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<bool> UnAttachTagFromAsset(int TagId, Guid assetId)
        {
            var assetTag = await _dbContext.AssetTags.FirstOrDefaultAsync(a => a.Tag_Id == TagId && a.Asset_Id == assetId);
            if (assetTag != null)
            {
                _dbContext.AssetTags.Remove(assetTag);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }
        public async Task<List<AssetTagEntity>> GetAssetTags(Guid assetId)
        {
            return _dbContext.AssetTags.Where(at => at.Asset_Id == assetId).ToList();
        }
        public async Task<int> GetOrCreateAssetTagId(string tagName)
        {
            return await _tagService.GetOrCreateTagId(tagName);
        }
    }
}

using Marketplace_3d_Assets.BusinessLogic.Interfaces;
using Marketplace_3d_Assets.Data;
using Marketplace_3d_Assets.DataAccess.Entities;
using Marketplace_3d_Assets.PresentationLayer.DTOs;
using Marketplace_3d_Assets.PresentationLayer.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Marketplace_3d_Assets.BusinessLogic.Services
{
    public class AssetCommentService : IAssetCommentService
    {
        private readonly ApplicationContext _dbContext;
        public AssetCommentService(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<CommentViewModel>> GetCommentsAsync(Guid assetId)
        {
            return await _dbContext.AssetComments
                .Where(c => c.Asset_Id == assetId)
                .OrderByDescending(c => c.Publication_Date)
                .Select(c => new CommentViewModel
                {
                    Comment_Id = c.Asset_Comment_Id,
                    User_Name = c.Profile.User_Name,
                    Comment_Text = c.Comment_Text,
                    Publication_Date = c.Publication_Date.ToString("yyyy-MM-dd HH:mm")
                })
                .ToListAsync();
        }
        public async Task<CommentsDto> GetCommentsByAssetIdPaged(Guid assetId, int page = 1, int pageSize = 10)
        {
            var query = _dbContext.AssetComments
                .Where(c => c.Asset_Id == assetId)
                .OrderByDescending(c => c.Publication_Date);

            var totalCount = await query.CountAsync();
            var comments = await query
                .Skip((page-1)*pageSize)
                .Take(pageSize)
                .Select(c => new CommentViewModel
                {
                    Comment_Id = c.Asset_Comment_Id,
                    User_Name = c.Profile.User_Name,
                    Comment_Text = c.Comment_Text,
                    Publication_Date = c.Publication_Date.ToString("yyyy-MM-dd HH:mm"),
                })
                .ToListAsync();
            return new CommentsDto
            {
                Comments = comments,
                totalCount = totalCount
            };
        }
        public async Task<CommentViewModel> CreateCommentAsync(CreateCommentDto commentDto, string authorUserName)
        {
            var userProfile = await _dbContext.UserProfiles.FirstOrDefaultAsync(p => p.User_Name == authorUserName);
            if (userProfile == null) throw new Exception("UserProfile not found");

            var comment = new AssetCommentEntity
            {
                Asset_Comment_Id = Guid.NewGuid(),
                Comment_Text = commentDto.TextContent,
                Publication_Date = DateTime.UtcNow,
                Asset_Id = commentDto.EntityId,
                Profile_Id = userProfile.Profile_Id,
                Profile = userProfile
            };

            _dbContext.AssetComments.Add(comment);
            await _dbContext.SaveChangesAsync();

            return new CommentViewModel
            {
                Comment_Id = comment.Asset_Comment_Id,
                Comment_Text = comment.Comment_Text,
                User_Name = userProfile.User_Name,
                Publication_Date = comment.Publication_Date.ToString("yyyy-MM-dd HH:mm")
            };
        }
        public async Task<bool> DeleteCommentAsync(Guid commentId)
        {
            var comment = await _dbContext.AssetComments.FindAsync(commentId);
            if (comment == null) return false;

            _dbContext.AssetComments.Remove(comment);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}

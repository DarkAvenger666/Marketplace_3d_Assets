using Marketplace_3d_Assets.Data;
using Marketplace_3d_Assets.PresentationLayer.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Marketplace_3d_Assets.BusinessLogic.Services
{
    public class StatisticsService
    {
        private readonly ApplicationContext _dbContext;

        public StatisticsService(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PostStatsViewModel> GetPostStatisticsAsync()
        {
            var oneMonthAgo = DateTime.UtcNow.AddMonths(-1);

            var posts = await _dbContext.Posts
                .Where(p => p.Publication_Date >= oneMonthAgo)
                .ToListAsync();

            return new PostStatsViewModel
            {
                TotalPosts = posts.Count
            };
        }

        public async Task<AssetStatsViewModel> GetAssetStatisticsAsync()
        {
            var oneMonthAgo = DateTime.UtcNow.AddMonths(-1);

            var assets = await _dbContext.Assets
                .Where(a => a.Upload_Date >= oneMonthAgo && a.Status_Id == 3)
                .ToListAsync();

            return new AssetStatsViewModel
            {
                TotalAssets = assets.Count,
                TotalComments = assets.Sum(a => a.Asset_Comments.Count),
                TotalLikes = assets.Sum(a => a.Asset_Likes.Count),
            };
        }
    }
}

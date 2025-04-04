using NuGet.ContentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marketplace_3d_Assets.DataAccess.Entities
{
    [Table("asset_like")]
    public class AssetLikeEntity
    {
        public Guid Asset_Id { get; set; }
        public Guid Profile_Id { get; set; }

        public AssetEntity Asset { get; set; }
        public UserProfileEntity Profile { get; set; }
    }
}

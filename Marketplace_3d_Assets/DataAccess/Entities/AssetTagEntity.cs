using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marketplace_3d_Assets.DataAccess.Entities
{
    [Table("asset_tag")]
    public class AssetTagEntity
    {
        public Guid Asset_Id { get; set; }
        public int Tag_Id { get; set; }
        public TagEntity Tag { get; set; }
        public AssetEntity Asset { get; set; }
    }
}

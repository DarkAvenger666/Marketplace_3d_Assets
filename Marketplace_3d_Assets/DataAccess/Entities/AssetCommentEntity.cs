using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marketplace_3d_Assets.DataAccess.Entities
{
    [Table("asset_comment")]
    public class AssetCommentEntity
    {
        [Key]
        public Guid Asset_Comment_Id { get; set; }
        public Guid Asset_Id { get; set; }
        //public AssetEntity? Asset { get; set; }
        public Guid Profile_Id { get; set; }
        //public UserProfileEntity? Profile { get; set; }
        public string Comment_Text { get; set; }
        public DateTime Publication_Date { get; set; }
    }
}

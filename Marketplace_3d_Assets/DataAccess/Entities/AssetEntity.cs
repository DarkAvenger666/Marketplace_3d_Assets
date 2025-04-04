using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marketplace_3d_Assets.DataAccess.Entities
{
    [Table("asset")]
    public class AssetEntity
    {
        [Key]
        public Guid Asset_Id { get; set; }
        public string Title { get; set; }
        public int Type_Id { get; set; }
        public AssetTypeEntity Type { get; set; }
        public DateTime Upload_Date { get; set; }
        public DateTime Modified_Date { get; set; }
        public int Status_Id { get; set; }
        public AssetStatusEntity Status { get; set; }
        public int Count_Of_Copies_Sold { get; set; }
        public Guid Profile_Id { get; set; }
        public UserProfileEntity Profile { get; set; }
        public string Asset_Description { get; set; }
        public float Price { get; set; }
        public int Count_Of_Views { get; set; }
        public List<AssetCommentEntity> Asset_Comments { get; set; } = new List<AssetCommentEntity>();
        public List<AssetImageEntity> Asset_Images { get; set; } = new List<AssetImageEntity>();
        public List<AssetTagEntity> Asset_Tags { get; set; } = new List<AssetTagEntity>();
        public List<AssetLikeEntity> Asset_Likes { get; set; } = new List<AssetLikeEntity>();
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marketplace_3d_Assets.DataAccess.Entities
{
    [Table("user_profile")]
    public class UserProfileEntity
    {
        [Key]
        public Guid Profile_Id { get; set; }
        public string User_Name { get; set; }
        public int Subscribers_Count { get; set; }
        public int Subscriptions_Count { get; set; }
        public DateTime Creation_Date { get; set; }
        public DateTime Modified_Date { get; set; }
        public string Gender { get; set; }
        public string Specialization { get; set; }
        public string City { get; set; }
        public string About { get; set; }
        public UserEntity User { get; set; }
        public List<AssetEntity> Assets { get; set; } = new List<AssetEntity>();
        public List<AssetCommentEntity> Asset_Comments { get; set; }
        public List<AssetLikeEntity> Asset_Likes { get; set; }
        public List<PostCommentEntity> Post_Comments { get; set; }
        public List<NetworkLinkEntity> Network_Links { get; set; }
        public List<PostEntity> Posts { get; set; }
    }
}

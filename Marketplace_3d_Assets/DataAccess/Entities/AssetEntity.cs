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
        //public AssetTypeEntity? Type { get; set; }
        public DateTime Upload_Date { get; set; }
        public DateTime Modified_Date { get; set; }
        public int Status_Id { get; set; }
        //public AssetStatusEntity? Status { get; set; }
        public int Count_Of_Copies_Sold { get; set; }
        public Guid Profile_Id { get; set; }
        //public UserProfileEntity? UserProfile { get; set; }
        public string Asset_Description { get; set; }
        public float Price { get; set; }
        public int Count_Of_Views { get; set; }
        public int Count_Of_Likes { get; set; }
    }
}

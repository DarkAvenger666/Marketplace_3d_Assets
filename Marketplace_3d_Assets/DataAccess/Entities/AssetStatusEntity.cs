using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marketplace_3d_Assets.DataAccess.Entities
{
    [Table("asset_status")]
    public class AssetStatusEntity
    {
        [Key]
        public int Status_Id { get; set; }
        public string Name { get; set; }
        public List<AssetEntity> Assets { get; set; } = new List<AssetEntity>();
    }
}

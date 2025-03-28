using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marketplace_3d_Assets.DataAccess.Entities
{
    [Table("asset_type")]
    public class AssetTypeEntity
    {
        [Key]
        public int Type_Id { get; set; }
        public string Name { get; set; }
    }
}

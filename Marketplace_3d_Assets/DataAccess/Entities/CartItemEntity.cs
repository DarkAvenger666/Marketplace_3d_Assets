using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marketplace_3d_Assets.DataAccess.Entities
{
    [Table("cart_item")]
    public class CartItemEntity
    {
        [Key]
        public Guid Cart_Item_Id { get; set; }
        public Guid User_Id { get; set; }
        public Guid Asset_Id { get; set; }
        public Guid Asset_Bundle_Id { get; set; }
    }
}

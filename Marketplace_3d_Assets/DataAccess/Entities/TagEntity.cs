using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marketplace_3d_Assets.DataAccess.Entities
{
    [Table("tag")]
    public class TagEntity
    {
        [Key]
        public int Tag_Id { get; set; }
        public string Name { get; set; }
    }
}

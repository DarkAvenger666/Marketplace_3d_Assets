using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marketplace_3d_Assets.DataAccess.Entities
{
    [Table("network_name")]
    public class NetworkNameEntity
    {
        [Key]
        public int Network_Name_Id { get; set; }
        public string Name { get; set; }
    }
}

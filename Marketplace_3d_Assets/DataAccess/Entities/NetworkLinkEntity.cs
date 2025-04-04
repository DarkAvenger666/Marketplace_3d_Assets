using System.ComponentModel.DataAnnotations.Schema;

namespace Marketplace_3d_Assets.DataAccess.Entities
{
    [Table("network_link")]
    public class NetworkLinkEntity
    {
        public Guid Profile_Id { get; set; }
        public int Network_Name_Id { get; set; }
        public string Profile_Link { get; set; }
        public UserProfileEntity User_Profile { get; set; }
    }
}

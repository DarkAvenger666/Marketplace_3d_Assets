using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marketplace_3d_Assets.DataAccess.Entities
{
    [Table("moderation_request")]
    public class ModerationRequestEntity
    {
        [Key]
        public Guid Request_Id { get; set; }
        public DateTime Sending_Date { get; set; }
        public DateTime Completion_Date { get; set; }
        public Guid Asset_Id { get; set; }
        public bool Is_Complited { get; set; }
        public string Comment { get; set; }
        public Guid User_Id { get; set; }
    }
}

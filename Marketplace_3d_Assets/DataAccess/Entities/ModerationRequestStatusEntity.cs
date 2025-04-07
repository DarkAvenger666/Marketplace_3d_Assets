using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Marketplace_3d_Assets.DataAccess.Entities
{
    [Table("moderation_request_status")]
    public class ModerationRequestStatusEntity
    {
        [Key]
        public int Status_Id { get; set; }
        public string Name { get; set; }
        public List<ModerationRequestEntity> moderationRequests { get; set; }
    }
}

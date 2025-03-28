using System.ComponentModel.DataAnnotations.Schema;

namespace Marketplace_3d_Assets.DataAccess.Entities
{
    [Table("subscription")]
    public class SubscriptionEntity
    {
        public Guid From_Whom_Profile_Id { get; set; }
        public Guid To_Whom_Profile_Id { get; set; }
    }
}

namespace Marketplace_3d_Assets.DataAccess.Entities
{
    public class ModerationRequestEntity
    {
        public Guid RequestId { get; set; }
        public DateTime SendingDate { get; set; }
        public DateTime CompletionDate { get; set; }
        public Guid AssetId { get; set; }
        public bool IsComplited { get; set; }
        public string Comment { get; set; }
        public Guid UserId { get; set; }
    }
}

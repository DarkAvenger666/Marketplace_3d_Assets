namespace Marketplace_3d_Assets.DataAccess.Entities
{
    public class UserProfileEntity
    {
        public Guid ProfileId { get; set; }
        public string UserName { get; set; }
        public int SubscribersCount { get; set; }
        public int SubscriptionCount { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string Gender { get; set; }
        public string Specialization { get; set; }
        public string City { get; set; }
        public string About { get; set; }
    }
}

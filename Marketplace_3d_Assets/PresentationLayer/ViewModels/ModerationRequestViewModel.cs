namespace Marketplace_3d_Assets.PresentationLayer.ViewModels
{
    public class ModerationRequestViewModel
    {
        public Guid RequestId { get; set; }
        public DateTime SendingDateTime { get; set; }
        public string AssetTitle { get; set; }
        public string AssetType { get; set; }
        public string AuthorUserName { get; set; }
    }
}

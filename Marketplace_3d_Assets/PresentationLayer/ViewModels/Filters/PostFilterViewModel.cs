namespace Marketplace_3d_Assets.PresentationLayer.ViewModels.Filters
{
    public class PostFilterViewModel
    {
        public string SearchQuery { get; set; }
        public DateTime? StartDate { get; set; }  
        public DateTime? EndDate { get; set; }    
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 5;
    }
}

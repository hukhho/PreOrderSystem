namespace PreOrderPlatform.Service.ViewModels.Campaign.Response
{
    public class CampaignResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid ProductId { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public int DepositPercent { get; set; }
        public DateTime? ExpectedShippingDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string Type { get; set; }
        public string Location { get; set; }
        public string Status { get; set; }
        public string Visibility { get; set; }
        // New properties for string representation
        //public string TypeString => Type.ToString();
        //public string LocationString => Location.ToString();
        //public string StatusString => Status.ToString();
        //public string VisibilityString => Visibility.ToString();

        public bool IsDeleted { get; set; }
        public Guid OwnerId { get; set; }
        public Guid BusinessId { get; set; }
    }
}

namespace PreOrderPlatform.Service.ViewModels.CampaignPrice.Response
{
    public class CampaignPriceResponse
    {
        public Guid Id { get; set; }
        public int Phase { get; set; }
        public string PhaseStatus { get; set; }
        public decimal Price { get; set; }
        public int AllowedQuantity { get; set; }
        public int TotalOrdered { get; set; }
        public Guid CampaignId { get; set; }
    }
}


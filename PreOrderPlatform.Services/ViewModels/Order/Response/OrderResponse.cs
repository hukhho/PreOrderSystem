namespace PreOrderPlatform.Service.ViewModels.Order.Response
{
    public class OrderResponse
    {
        public Guid Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? TotalQuantity { get; set; }
        public decimal? TotalPrice { get; set; }
        public bool IsDeposited { get; set; }
        public decimal? RequiredDepositAmount { get; set; }
        public string? RevicerName { get; set; }
        public string? RevicerPhone { get; set; }
        public string? ShippingAddress { get; set; }
        public string? ShippingProvince { get; set; }
        public string? ShippingWard { get; set; }
        public string? ShippingDistrict { get; set; }
        public string? ShippingCode { get; set; }
        public decimal? ShippingPrice { get; set; }
        public string? ShippingStatus { get; set; }
        public Guid? UserId { get; set; }
    }
}

namespace PreOrderPlatform.Service.ViewModels.Order.Request
{
    public class OrderSearchRequest
    {
        public DateTime? StartDateInRange { get; set; }
        public DateTime? EndDateInRange { get; set; }
        public bool? IsDeposited { get; set; }
        public string? Status { get; set; }
        public string? RevicerName { get; set; }
        public string? RevicerPhone { get; set; }
        public Guid? UserId { get; set; }
        public string? ShippingStatus { get; set; }
        public string? ShippingAddress { get; set; }
        public string? ShippingProvince { get; set; }
        public string? ShippingWard { get; set; }
        public string? ShippingDistrict { get; set; }
        public string? ShippingCode { get; set; }
        public Guid? BusinessId { get; set; }
    }
}

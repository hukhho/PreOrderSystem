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
    }
}

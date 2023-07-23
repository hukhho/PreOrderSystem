using PreOrderPlatform.Entity.Enum.Order;

namespace PreOrderPlatform.Service.ViewModels.Order
{
    public class OrderUpdateViewModel
    {
        public Guid Id { get; set; }
        public decimal? IsDeposited { get; set; }
        public string? RevicerName { get; set; }
        public string? RevicerPhone { get; set; }
        public ShippingStatus? ShippingStatus { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime? EstimatedDeliveryDate { get; set; }
        public string? Note { get; set; }

        public string? ShippingAddress { get; set; }
        public string? ShippingProvince { get; set; }
        public string? ShippingWard { get; set; }
        public string? ShippingDistrict { get; set; }
        public string? ShippingCode { get; set; }
        public decimal? ShippingPrice { get; set; }
        public Guid? UserId { get; set; }
    }
}

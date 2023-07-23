using System.ComponentModel.DataAnnotations;
using PreOrderPlatform.Service.ViewModels.OrderItem;
using PreOrderPlatform.Service.ViewModels.Payment;

namespace PreOrderPlatform.Service.ViewModels.Order
{
    public class OrderCreateViewModel
    {
        public string? RevicerName { get; set; }
        public string? RevicerPhone { get; set; }
        public string? ShippingAddress { get; set; }
        public string? ShippingProvince { get; set; }
        public string? ShippingWard { get; set; }
        public string? ShippingDistrict { get; set; }
        public string? Note { get; set; }

        // Order items
        [Required]
        public List<OrderItemCreateViewModel> OrderItems { get; set; }

        // Payment
        [Required]
        public List<PaymentCreateViewModel> Payments { get; set; }


    }
}

using PreorderPlatform.Service.ViewModels.OrderItem;
using PreorderPlatform.Service.ViewModels.Payment;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreorderPlatform.Service.ViewModels.Order
{
    public class OrderCreateViewModel
    {
        public string? RevicerName { get; set; }
        public string? RevicerPhone { get; set; }
        public string? ShippingAddress { get; set; }
        public string? ShippingProvince { get; set; }
        public string? ShippingWard { get; set; }
        public string? ShippingDistrict { get; set; }

        // Order items
        [Required]
        public List<OrderItemCreateViewModel> OrderItems { get; set; }

        // Payment
        [Required]
        public List<PaymentCreateViewModel> Payments { get; set; }


    }
}

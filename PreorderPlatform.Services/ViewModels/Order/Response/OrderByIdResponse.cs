using PreorderPlatform.Service.ViewModels.OrderItem.Response;
using PreorderPlatform.Service.ViewModels.Payment.Response;
using PreorderPlatform.Service.ViewModels.User.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreorderPlatform.Service.ViewModels.Order.Response
{
    public class OrderByIdResponse
    {
        public Guid Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? TotalQuantity { get; set; }
        public decimal? TotalPrice { get; set; }
        public bool IsDeposited { get; set; }
        public decimal? RequiredDepositAmount { get; set; }
        public string? Status { get; set; }
        public string? RevicerName { get; set; }
        public string? RevicerPhone { get; set; }
        public string? ShippingAddress { get; set; }
        public string? ShippingProvince { get; set; }
        public string? ShippingWard { get; set; }
        public string? ShippingDistrict { get; set; }
        public string? ShippingCode { get; set; }
        public decimal? ShippingPrice { get; set; }
        public string? ShippingStatus { get; set; }

        public virtual UserResponse? User { get; set; }
        public virtual ICollection<OrderItemResponse> OrderItems { get; set; }
        public virtual ICollection<PaymentResponse> Payments { get; set; }
    }
}

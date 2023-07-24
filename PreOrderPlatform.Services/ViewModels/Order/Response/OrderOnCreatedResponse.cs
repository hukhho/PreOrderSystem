using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PreOrderPlatform.Entity.Enum.Order;
using PreOrderPlatform.Entity.Models;
using PreOrderPlatform.Service.ViewModels.BusinessPaymentCredential;
using PreOrderPlatform.Service.ViewModels.OrderItem.Response;
using PreOrderPlatform.Service.ViewModels.Payment.Response;

namespace PreOrderPlatform.Service.ViewModels.Order.Response
{
    public class OrderOnCreatedResponse
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public int TotalQuantity { get; set; }
        public decimal TotalPrice { get; set; }

        public bool IsDeposited { get; set; }
        public decimal RequiredDepositAmount { get; set; }
        public string? RevicerName { get; set; }
        public string? RevicerPhone { get; set; }
        public string? ShippingAddress { get; set; }
        public string? ShippingProvince { get; set; }
        public string? ShippingWard { get; set; }
        public string? ShippingDistrict { get; set; }
        public string? ShippingCode { get; set; }
        public decimal? ShippingPrice { get; set; }
        public string? ShippingStatus { get; set; }
        public string? Status { get; set; }
        public DateTime? EstimatedDeliveryDate { get; set; }
        public DateTime? CancelledAt { get; set; }
        public string? CancelledBy { get; set; }
        public string? CancelledReason { get; set; }
        public string? UpdatedBy { get; set; }
        public string? Note { get; set; }
        public Guid UserId { get; set; }
        //public virtual UserResponse? User { get; set; }
        public virtual ICollection<OrderItemResponse> OrderItems { get; set; }
        public virtual ICollection<PaymentResponse> Payments { get; set; }
        [JsonIgnore]
        public BusinessPaymentCredentialViewModel businessPaymentCredential { get; set; }
        public JObject paymentUrl { get; set;}
    }
}

using PreorderPlatform.Entity.Repositories.Enum.Order;
using System;
using System.Collections.Generic;

namespace PreorderPlatform.Entity.Models
{
    public partial class Order
    {
        public Order()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(7)).DateTime;
            OrderItems = new HashSet<OrderItem>();
            Payments = new HashSet<Payment>();
        }

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
        public ShippingStatus? ShippingStatus { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime? EstimatedDeliveryDate { get; set; }
        public DateTime? CancelledAt { get; set; }
        public string? CancelledBy { get; set; }
        public string? CancelledReason { get; set; }
        public string? UpdatedBy { get; set; }
        public string? Note { get; set; }
        public Guid UserId { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}

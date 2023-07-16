using System;
using System.Collections.Generic;

namespace PreorderPlatform.Entity.Models
{
    public partial class Order
    {
        public Order()
        {
            Id = Guid.NewGuid();
            OrderItems = new HashSet<OrderItem>();
            Payments = new HashSet<Payment>();
        }

        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public int TotalQuantity { get; set; }
        public decimal TotalPrice { get; set; }

        public bool IsDeposited { get; set; }
        public decimal RequiredDepositAmount { get; set; }
        public string Status { get; set; }
        public string? RevicerName { get; set; }
        public string? RevicerPhone { get; set; }
        public string? ShippingAddress { get; set; }
        public string? ShippingProvince { get; set; }
        public string? ShippingWard { get; set; }
        public string? ShippingDistrict { get; set; }
        public string? ShippingCode { get; set; }
        public decimal? ShippingPrice { get; set; }
        public string? ShippingStatus { get; set; }
        public Guid UserId { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}

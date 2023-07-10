using System;
using System.Collections.Generic;

namespace PreorderPlatform.Entity.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderItems = new HashSet<OrderItem>();
            Payments = new HashSet<Payment>();
        }

        public int Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? TotalQuantity { get; set; }
        public decimal? TotalPrice { get; set; }
        public decimal? IsDeposited { get; set; }
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
        public int? UserId { get; set; }

        public virtual User? User { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }

        public override string ToString()
        {
            // Customize the string representation of the Order instance
            // Return the desired properties or values you want to print

            return $"Order ID: {Id}, " +
                   $"Created At: {CreatedAt}, " +
                   $"Total Quantity: {TotalQuantity}, " +
                   $"Total Price: {TotalPrice}, " +
                   $"Is Deposited: {IsDeposited}, " +
                   $"Status: {Status}, " +
                   $"Receiver Name: {RevicerName}, " +
                   $"Receiver Phone: {RevicerPhone}, " +
                   $"Shipping Address: {ShippingAddress}, " +
                   $"Shipping Province: {ShippingProvince}, " +
                   $"Shipping Ward: {ShippingWard}, " +
                   $"Shipping District: {ShippingDistrict}, " +
                   $"Shipping Code: {ShippingCode}, " +
                   $"Shipping Price: {ShippingPrice}, " +
                   $"Shipping Status: {ShippingStatus}, " +
                   $"User ID: {UserId}";
        }
    }
}

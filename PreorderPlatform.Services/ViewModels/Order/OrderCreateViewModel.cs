﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreorderPlatform.Service.ViewModels.Order
{
    public class OrderCreateViewModel
    {
        public DateTime? CreatedAt { get; set; }
        public Guid? TotalQuantity { get; set; }
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
        public Guid? UserId { get; set; }
    }
}

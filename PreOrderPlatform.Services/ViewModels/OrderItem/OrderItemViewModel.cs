﻿namespace PreOrderPlatform.Service.ViewModels.OrderItem
{
    public class OrderItemViewModel
    {
        public Guid Id { get; set; }
        public Guid? CampaignDetailId { get; set; }
        public int? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public Guid? OrderId { get; set; }
    }
}

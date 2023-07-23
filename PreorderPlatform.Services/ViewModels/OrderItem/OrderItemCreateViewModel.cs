using System.ComponentModel.DataAnnotations;

namespace PreOrderPlatform.Service.ViewModels.OrderItem
{
    public class OrderItemCreateViewModel
    {
        [Required]
        public Guid CampaignDetailId { get; set; }

        //public Guid? CampaignDetailId { get; set; }
        [Range(1, int.MaxValue)]
        public int? Quantity { get; set; }
    }
}

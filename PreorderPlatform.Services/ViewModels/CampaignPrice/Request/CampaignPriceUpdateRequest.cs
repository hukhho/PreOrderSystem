using System.ComponentModel.DataAnnotations;

namespace PreOrderPlatform.Service.ViewModels.CampaignPrice.Request
{
    public class CampaignPriceUpdateRequest
    {
        [Required] 
        public Guid Id { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Allowed Quantity must be a positive number.")]
        public int AllowedQuantity { get; set; }

        //[JsonIgnore]
        //public Guid CampaignId { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be a positive number.")]
        public decimal Price { get; set; }

    }    
}

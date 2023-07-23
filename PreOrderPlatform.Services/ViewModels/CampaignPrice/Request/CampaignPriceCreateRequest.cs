using System.ComponentModel.DataAnnotations;

namespace PreOrderPlatform.Service.ViewModels.CampaignPrice.Request
{
    public class CampaignPriceCreateRequest
    {
        // Request properties
        [Required]
        [Range(1, 10, ErrorMessage = "Phase must be a positive number. Phase must be <= 10")]
        public int Phase { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "AllowedQuantity must be a positive number.")]

        public int AllowedQuantity { get; set; }
      

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be a positive number.")] public decimal Price { get; set; }
    }
}

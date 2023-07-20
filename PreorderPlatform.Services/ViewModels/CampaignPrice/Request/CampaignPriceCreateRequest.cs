using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreorderPlatform.Service.ViewModels.CampaignPrice.Request
{
    public class CampaignPriceCreateRequest
    {
        // Request properties
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "AllowedQuantity must be a positive number.")]
        public int AllowedQuantity { get; set; }
        [JsonIgnore]
        public Guid CampaignId { get; set; }
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be a positive number.")] public decimal Price { get; set; }
    }
}

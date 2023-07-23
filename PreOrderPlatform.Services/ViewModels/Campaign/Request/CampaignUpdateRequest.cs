using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PreOrderPlatform.Service.ViewModels.Campaign.Request
{
    public class CampaignUpdateRequest
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime StartAt { get; set; }
        [Required]
        public DateTime EndAt { get; set; }
        [Required]
        public int DepositPercent { get; set; }
        [Required]
        public DateTime ExpectedShippingDate { get; set; }
       
    }
}

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using PreOrderPlatform.Entity.Enum.Campaign;

namespace PreOrderPlatform.Service.ViewModels.Campaign.Request
{
    public class CampaignSearchRequest
    {
        public string? Name { get; set; }

        [DataType(DataType.Date)]
        [Description("Enter the date in the format 'YYYY-MM-DD'")]
        [FromQuery]
        public DateTime? DateInRange { get; set; }
        public CampaignType? Type { get; set; }
        public CampaignLocation? Location { get; set; }
        public CampaignStatus? Status { get; set; }
        public Guid? OwnerId { get; set; }
        public Guid? BusinessId { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreorderPlatform.Service.ViewModels.Campaign.Request
{
    public class CampaignCreateRequest
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime? StartAt { get; set; }
        public DateTime? EndAt { get; set; }
        public int? DepositPercent { get; set; }
        public DateTime? ExpectedShippingDate { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public bool? Status { get; set; }
        public Guid? OwnerId { get; set; }
        public Guid? BusinessId { get; set; }
    }
}

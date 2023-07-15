using System;
using System.Collections.Generic;

namespace PreorderPlatform.Entity.Models
{
    public partial class Campaign
    {
        public Campaign()
        {
            CampaignDetails = new HashSet<CampaignDetail>();
        }

        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public Guid? ProductId { get; set; }
        public DateTime? StartAt { get; set; }
        public DateTime? EndAt { get; set; }
        public int? DepositPercent { get; set; }
        public DateTime? ExpectedShippingDate { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public bool? Status { get; set; }
        public Guid? OwnerId { get; set; }
        public Guid? BusinessId { get; set; }

        public virtual Business? Business { get; set; }
        public virtual User? Owner { get; set; }
        public virtual Product? Product { get; set; }
        public virtual ICollection<CampaignDetail> CampaignDetails { get; set; }
    }
}

using PreorderPlatform.Entity.Repositories.Enum.Campaign;
using PreorderPlatform.Entity.Repositories.Enum.Status;
using PreorderPlatform.Entity.Repositories.Enum.Visibility;
using System;
using System.Collections.Generic;

namespace PreorderPlatform.Entity.Models
{
    public partial class Campaign
    {
        public Campaign()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(7)).DateTime;
            CampaignDetails = new HashSet<CampaignDetail>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid ProductId { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public int DepositPercent { get; set; }
        public DateTime? ExpectedShippingDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public CampaignType Type { get; set; }
        public CampaignLocation Location { get; set; }
        public CampaignStatus Status { get; set; }
        public Visibility Visibility { get; set; }
        public bool IsDeleted { get; set; }
        public Guid OwnerId { get; set; }
        public Guid BusinessId { get; set; }
        public virtual Business Business { get; set; }
        public virtual User Owner { get; set; }
        public virtual Product Product { get; set; }
        public virtual ICollection<CampaignImage> Images { get; set; }
        public virtual ICollection<CampaignDetail> CampaignDetails { get; set; }
    }
}

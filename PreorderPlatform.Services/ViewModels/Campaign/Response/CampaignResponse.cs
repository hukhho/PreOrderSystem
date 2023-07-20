using PreorderPlatform.Entity.Repositories.Enum.Campaign;
using PreorderPlatform.Entity.Repositories.Enum.Status;
using PreorderPlatform.Entity.Repositories.Enum.Visibility;
using PreorderPlatform.Service.ViewModels.Business.Response;
using PreorderPlatform.Service.ViewModels.CampaignPrice.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreorderPlatform.Service.ViewModels.Campaign.Response
{
    public class CampaignResponse
    {
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
        public string Type { get; set; }
        public string Location { get; set; }
        public string Status { get; set; }
        public string Visibility { get; set; }
        // New properties for string representation
        //public string TypeString => Type.ToString();
        //public string LocationString => Location.ToString();
        //public string StatusString => Status.ToString();
        //public string VisibilityString => Visibility.ToString();

        public bool IsDeleted { get; set; }
        public Guid OwnerId { get; set; }
        public Guid BusinessId { get; set; }
    }
}

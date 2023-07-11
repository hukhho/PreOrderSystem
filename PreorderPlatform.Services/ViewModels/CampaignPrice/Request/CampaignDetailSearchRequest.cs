
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreorderPlatform.Service.ViewModels.CampaignPrice.Request
{
    /// <summary>
    /// Represents the request for updating a campaign price.
    /// </summary>
    public class CampaignDetailSearchRequest
    { 
        public int? Phase { get; set; }
        public int? CampaignId { get; set; }
    }    
}

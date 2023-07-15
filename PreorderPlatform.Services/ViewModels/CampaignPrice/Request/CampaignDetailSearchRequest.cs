
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreorderPlatform.Service.ViewModels.CampaignPrice.Request
{

    public class CampaignDetailSearchRequest
    { 
        public Guid? PhaseId { get; set; }
        public Guid? CampaignId { get; set; }
    }    
}

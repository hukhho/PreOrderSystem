﻿
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
    public class CampaignPriceUpdateRequest
    { 
        public int Id { get; set; }
        public int? Phase { get; set; }
        public int? AllowedQuantity { get; set; }
        public int? TotalOrdered { get; set; }
        public int? CampaignId { get; set; }
        public decimal? Price { get; set; }
    }    
}

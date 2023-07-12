﻿using PreorderPlatform.Service.ViewModels.Business.Response;
using PreorderPlatform.Service.ViewModels.Campaign.Response;
using PreorderPlatform.Service.ViewModels.category.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreorderPlatform.Service.ViewModels.Product.Response
{
    public class ProductByIdResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Image { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public bool? Status { get; set; }

        public virtual BusinessResponse? Business { get; set; }
        public virtual CategoryResponse? Category { get; set; }
        public virtual ICollection<CampaignResponse> Campaigns { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreorderPlatform.Entity.Models
{
    public partial class CampaignImage
    {
        public CampaignImage()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public bool IsThumbnail { get; set; }
        public int Order { get; set; }
        public Guid CampaignId { get; set; }

        public virtual Campaign Campaign { get; set; }
    }
}

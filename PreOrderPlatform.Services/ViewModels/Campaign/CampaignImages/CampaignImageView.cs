﻿namespace PreOrderPlatform.Service.ViewModels.Campaign.CampaignImages
{
    public class CampaignImageView
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public bool IsThumbnail { get; set; }
        public int Order { get; set; }
    }
}

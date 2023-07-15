﻿using PreorderPlatform.Entity.Models;

namespace PreorderPlatform.Entity.Repositories.CampaignDetailRepositories
{
    public class CampaignDetailRepository : RepositoryBase<CampaignDetail>, ICampaignDetailRepository
    {
        public CampaignDetailRepository(PreOrderSystemContext context) : base(context)
        {

        }

        // Add any additional methods specific to CampaignDetailRepository here...
        public async Task<IEnumerable<CampaignDetail>> GetAllCampainDetailsAsync()
        {
            return await GetAllAsync();
        }

        // Edit: Convert int id to Guid
        public async Task<CampaignDetail> GetCampaignDetailByIdAsync(Guid campaignDetailId)
        {
            return await GetByIdAsync(campaignDetailId);
        }
    }
}
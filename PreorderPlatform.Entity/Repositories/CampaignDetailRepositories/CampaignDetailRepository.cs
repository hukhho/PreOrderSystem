using Microsoft.EntityFrameworkCore;
using PreOrderPlatform.Entity.Models;

namespace PreOrderPlatform.Entity.Repositories.CampaignDetailRepositories
{
    public class CampaignDetailRepository : RepositoryBase<CampaignDetail>, ICampaignDetailRepository
    {
        private readonly PreOrderSystemContext _context;

        public CampaignDetailRepository(PreOrderSystemContext context) : base(context)
        {
            _context = context;
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


        public async Task<bool> AreAllCampaignDetailsInBusinessAsync(IEnumerable<Guid> campaignDetailIds)
        {
            // Fetch all campaign details at once
            var selectedCampaignDetails = await GetAllWithIncludeAsync(
                cd => campaignDetailIds.Contains(cd.Id),
                cd => cd.Campaign,
                cd => cd.Campaign.Business);

            if (selectedCampaignDetails.Count() != campaignDetailIds.Count())
            {
                return false; // Return false if any selected campaign detail was not found
            }

            // Get the ID of the business of the first campaign detail
            var firstBusinessId = selectedCampaignDetails.First().Campaign.Business.Id;

            // Check if all selected campaign details belong to the business with the firstBusinessId
            return selectedCampaignDetails.All(cd => cd.Campaign.Business.Id == firstBusinessId);
        }


        public async Task<int> GetMaxPhaseAsync()
        {
            return await _context.CampaignDetails.MaxAsync(detail => detail.Phase);
        }
    }
}
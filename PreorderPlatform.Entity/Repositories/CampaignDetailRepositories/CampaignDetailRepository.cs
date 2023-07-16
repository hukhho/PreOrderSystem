using Microsoft.EntityFrameworkCore;
using PreorderPlatform.Entity.Models;

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


        public async Task<bool> AreAllCampaignDetailsInBusinessAsync(IEnumerable<Guid> campaignDetailIds)
        {
            // Assuming GetWithIncludeAsync returns campaign details with the related Campaign and Business entities
            var selectedCampaignDetails = await Task.WhenAll(campaignDetailIds.Select(id =>
                GetWithIncludeAsync(cd => cd.Id == id, cd => cd.Include(d => d.Campaign).ThenInclude(c => c.Business))));

            if (selectedCampaignDetails.Any(cd => cd == null))
            {
                return false; // Return false if any selected campaign detail was not found
            }

            // Get the ID of the business of the first campaign detail
            var firstBusinessId = selectedCampaignDetails[0].Campaign.Business.Id;

            // Check if all selected campaign details belong to the business with the firstBusinessId
            return selectedCampaignDetails.All(cd => cd.Campaign.Business.Id == firstBusinessId);
        }
    }
}
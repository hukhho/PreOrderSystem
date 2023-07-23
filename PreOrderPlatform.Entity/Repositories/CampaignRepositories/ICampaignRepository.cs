using PreOrderPlatform.Entity.Models;

namespace PreOrderPlatform.Entity.Repositories.CampaignRepositories
{
    public interface ICampaignRepository : IRepositoryBase<Campaign>
    {
        Task<IEnumerable<Campaign>> GetAllCampaignsWithOwnerAndBusinessAndCampaignDetailsAsync();
        Task<Campaign> GetCampaignWithDetailsAsync(Guid id);
        Task<bool> IsOwnerOrStaff(Guid userId, Guid campaignId);
    }
}

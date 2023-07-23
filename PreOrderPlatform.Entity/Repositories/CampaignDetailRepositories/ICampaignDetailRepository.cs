using PreOrderPlatform.Entity.Models;

namespace PreOrderPlatform.Entity.Repositories.CampaignDetailRepositories
{
    public interface ICampaignDetailRepository : IRepositoryBase<CampaignDetail>
    {
        Task<bool> AreAllCampaignDetailsInBusinessAsync(IEnumerable<Guid> campaignDetailIds);
        Task<IEnumerable<CampaignDetail>> GetAllCampainDetailsAsync();
        Task<int> GetMaxPhaseAsync();
    }
}

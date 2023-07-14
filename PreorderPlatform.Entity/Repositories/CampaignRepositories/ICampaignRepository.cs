using PreorderPlatform.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreorderPlatform.Entity.Repositories.CampaignRepositories
{
    public interface ICampaignRepository : IRepositoryBase<Campaign>
    {
        Task<IEnumerable<Campaign>> GetAllCampaignsWithOwnerAndBusinessAndCampaignDetailsAsync();
        Task<Campaign> GetCampaignWithDetailsAsync(int id);
        Task<bool> IsOwnerOrStaff(int userId, int campaignId);
    }
}

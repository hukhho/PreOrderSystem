using PreorderPlatform.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreorderPlatform.Entity.Repositories.CampaignDetailRepositories
{
    public interface ICampaignDetailRepository : IRepositoryBase<CampaignDetail>
    {
        Task<bool> AreAllCampaignDetailsInBusinessAsync(IEnumerable<Guid> campaignDetailIds);
        Task<IEnumerable<CampaignDetail>> GetAllCampainDetailsAsync();
    }
}

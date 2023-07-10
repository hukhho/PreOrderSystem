using PreorderPlatform.Entity.Models;

namespace PreorderPlatform.Entity.Repositories.BusinessRepositories
{
    public class BusinessRepository : RepositoryBase<Business>, IBusinessRepository
    {
        public BusinessRepository(PreOrderSystemContext context) : base(context)
        {

        }

        // Add any additional methods specific to BusinessRepository here...
    }
}
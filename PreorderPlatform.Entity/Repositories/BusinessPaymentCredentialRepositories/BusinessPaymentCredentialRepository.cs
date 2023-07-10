using PreorderPlatform.Entity.Models;

namespace PreorderPlatform.Entity.Repositories.BusinessPaymentCredentialRepositories
{
    public class BusinessPaymentCredentialRepository : RepositoryBase<BusinessPaymentCredential>, IBusinessPaymentCredentialRepository
    {
        public BusinessPaymentCredentialRepository(PreOrderSystemContext context) : base(context)
        {

        }

        // Add any additional methods specific to BusinessPaymentCredentialRepository here...
    }
}
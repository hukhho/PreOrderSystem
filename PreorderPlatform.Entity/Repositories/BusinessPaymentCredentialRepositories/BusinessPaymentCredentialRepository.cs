using Microsoft.EntityFrameworkCore;
using PreorderPlatform.Entity.Models;

namespace PreorderPlatform.Entity.Repositories.BusinessPaymentCredentialRepositories
{
    public class BusinessPaymentCredentialRepository : RepositoryBase<BusinessPaymentCredential>, IBusinessPaymentCredentialRepository
    {
        public BusinessPaymentCredentialRepository(PreOrderSystemContext context) : base(context)
        {

        }

        // Add any additional methods specific to BusinessPaymentCredentialRepository here...
        public async Task<BusinessPaymentCredential> GetBusinessPaymentByIdAsync(int id)
        {
            var businessPayments = await GetWithIncludeAsync(
                bp => bp.Id == id,
                bp => bp.Include(b => b.Business)
                );
            return businessPayments;
        }
    }
}
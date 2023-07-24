using Microsoft.EntityFrameworkCore;
using PreOrderPlatform.Entity.Models;

namespace PreOrderPlatform.Entity.Repositories.BusinessPaymentCredentialRepositories
{
    public class BusinessPaymentCredentialRepository : RepositoryBase<BusinessPaymentCredential>, IBusinessPaymentCredentialRepository
    {
        public BusinessPaymentCredentialRepository(PreOrderSystemContext context) : base(context)
        {

        }


        public async Task<BusinessPaymentCredential> GetBusinessPaymentCredentialByIdAsync(Guid id)
        {
            var businessPayment = await GetWithIncludeAsync(
                b => b.Id == id,
                b => b.Include(o => o.Business)

                );

            //if (businesses != null)
            //{
            //    businesses.Users = businesses.Users.Where(user => user.RoleId != 2).ToList();
            //}
            return businessPayment;
        }


        public async Task<bool> IsBusinessPaymentCredentialInBusiness(Guid businessId, Guid credentialsId)
        {
            var businessPaymentCredential = await GetWithIncludeAsync(
                b => b.Id == credentialsId,
                b => b.Include(o => o.Business)
                );
            if (businessPaymentCredential != null)
            {
                return businessPaymentCredential.BusinessId == businessId;

            }
            else
            {
                return false;
            }
        }


        public async Task<BusinessPaymentCredential> GetMainBusinessPaymentCredentialByBusinessIdAsync(Guid businessId)
        {
            Console.WriteLine($"Starting GetMainBusinessPaymentCredentialByBusinessIdAsync with BusinessId: {businessId}");

            var businessPaymentCredential = await GetWithIncludeAsync(
                                       b => b.IsMain == true && b.BusinessId == businessId,
                                       b => b.Include(o => o.Business)
                                                     );

            if (businessPaymentCredential != null)
            {
                Console.WriteLine($"Successfully retrieved BusinessPaymentCredential with ID: {businessPaymentCredential.Id}");
            }
            else
            {
                Console.WriteLine($"No BusinessPaymentCredential found for BusinessId: {businessId}");
            }

            return businessPaymentCredential;
        }
        // Add any additional methods specific to BusinessPaymentCredentialRepository here...
    }
}
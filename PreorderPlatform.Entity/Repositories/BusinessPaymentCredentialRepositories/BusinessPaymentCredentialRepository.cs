using Microsoft.EntityFrameworkCore;
using PreorderPlatform.Entity.Models;

namespace PreorderPlatform.Entity.Repositories.BusinessPaymentCredentialRepositories
{
    public class BusinessPaymentCredentialRepository : RepositoryBase<BusinessPaymentCredential>, IBusinessPaymentCredentialRepository
    {
        public BusinessPaymentCredentialRepository(PreOrderSystemContext context) : base(context)
        {

        }


        public async Task<BusinessPaymentCredential> GetBusinessPaymentCredentialByIdAsync(int id)
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

        // Add any additional methods specific to BusinessPaymentCredentialRepository here...
    }
}
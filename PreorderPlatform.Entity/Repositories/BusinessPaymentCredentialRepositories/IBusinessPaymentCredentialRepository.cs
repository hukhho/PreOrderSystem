using PreorderPlatform.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreorderPlatform.Entity.Repositories.BusinessPaymentCredentialRepositories
{
    public interface IBusinessPaymentCredentialRepository : IRepositoryBase<BusinessPaymentCredential>
    {
        Task<BusinessPaymentCredential> GetBusinessPaymentCredentialByIdAsync(int id);
    }
}

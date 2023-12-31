﻿using PreOrderPlatform.Entity.Models;

namespace PreOrderPlatform.Entity.Repositories.BusinessPaymentCredentialRepositories
{
    public interface IBusinessPaymentCredentialRepository : IRepositoryBase<BusinessPaymentCredential>
    {
        Task<BusinessPaymentCredential> GetBusinessPaymentCredentialByIdAsync(Guid id);
        Task<BusinessPaymentCredential> GetMainBusinessPaymentCredentialByBusinessIdAsync(Guid businessId);
        Task<bool> IsBusinessPaymentCredentialInBusiness(Guid businessId, Guid credentialsId);
    }
}

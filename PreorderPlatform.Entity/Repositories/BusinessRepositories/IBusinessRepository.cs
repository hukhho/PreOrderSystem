using PreOrderPlatform.Entity.Models;

namespace PreOrderPlatform.Entity.Repositories.BusinessRepositories
{
    public interface IBusinessRepository : IRepositoryBase<Business>
    {
        Task<Business> GetBusinessByIdAsync(Guid id);
        Task<Business> GetByOwnerIdAsync(Guid userId);
        Task<bool> IsUserInBusiness(Guid userId, Guid businessId);
        Task<bool> IsUserOwnerOfBusiness(Guid userId, Guid businessId);
    }
}

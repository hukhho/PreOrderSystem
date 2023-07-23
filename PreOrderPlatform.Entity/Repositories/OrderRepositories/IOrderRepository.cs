using PreOrderPlatform.Entity.Models;

namespace PreOrderPlatform.Entity.Repositories.OrderRepositories
{
    public interface IOrderRepository : IRepositoryBase<Order>
    {
        Task<Order> GetOrderByIdAsync(Guid orderId);
        Task<bool> IsUserCanAccessOrder(Guid userId, Guid orderId);
    }
}

using Microsoft.EntityFrameworkCore;
using PreorderPlatform.Entity.Models;

namespace PreorderPlatform.Entity.Repositories.OrderRepositories
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(PreOrderSystemContext context) : base(context)
        {

        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            var order = await GetWithIncludeAsync(
                o => o.Id == id,
                o => o.Include(u => u.User),
                o => o.Include(u => u.OrderItems),
                o => o.Include(u => u.Payments)
                );
            return order;
        }

        // Add any additional methods specific to OrderRepository here...
    }
}
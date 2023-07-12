using Microsoft.EntityFrameworkCore;
using PreorderPlatform.Entity.Models;

namespace PreorderPlatform.Entity.Repositories.OrderItemRepositories
{
    public class OrderItemRepository : RepositoryBase<OrderItem>, IOrderItemRepository
    {
        public OrderItemRepository(PreOrderSystemContext context) : base(context)
        {

        }

        // Add any additional methods specific to OrderItemRepository here...
        public async Task<OrderItem> GetOrderItemByIdAsync(int id)
        {
            var orderItems = await GetWithIncludeAsync(
                oi => oi.Id == id,
                oi => oi.Include(cd => cd.CampaignDetail),
                oi => oi.Include(o => o.Order)
                );
            return orderItems;
        }
    }
}
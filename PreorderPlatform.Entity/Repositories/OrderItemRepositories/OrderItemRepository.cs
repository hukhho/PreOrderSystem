using PreorderPlatform.Entity.Models;

namespace PreorderPlatform.Entity.Repositories.OrderItemRepositories
{
    public class OrderItemRepository : RepositoryBase<OrderItem>, IOrderItemRepository
    {
        public OrderItemRepository(PreOrderSystemContext context) : base(context)
        {

        }

        // Add any additional methods specific to OrderItemRepository here...
    }
}
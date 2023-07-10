using PreorderPlatform.Entity.Models;

namespace PreorderPlatform.Entity.Repositories.OrderRepositories
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(PreOrderSystemContext context) : base(context)
        {

        }

        // Add any additional methods specific to OrderRepository here...
    }
}
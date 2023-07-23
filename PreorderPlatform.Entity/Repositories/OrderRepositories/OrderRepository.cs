using Microsoft.EntityFrameworkCore;
using PreOrderPlatform.Entity.Models;

namespace PreOrderPlatform.Entity.Repositories.OrderRepositories
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        private readonly PreOrderSystemContext _context;

        public OrderRepository(PreOrderSystemContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Order> GetOrderByIdAsync(Guid id)
        {
            var order = await GetWithIncludeAsync(
                o => o.Id == id,
                o => o.Include(u => u.User),
                o => o.Include(u => u.OrderItems),
                o => o.Include(u => u.Payments)
                );
            return order;
        }

        public async Task<bool> IsUserCanAccessOrder(Guid userId, Guid orderId)
        {
            // Check if the user is the owner of the order
            bool isUserOrderOwner = await _context.Orders.AnyAsync(o => o.Id == orderId && o.UserId == userId);

            if (isUserOrderOwner)
            {
                return true;
            }

            // Check if the user is the owner of the campaign or the business, or a staff member of the business
            bool canAccessOrder = await _context.OrderItems
                .Include(oi => oi.CampaignDetail)
                .ThenInclude(cd => cd.Campaign)
                .ThenInclude(c => c.Business)
                .ThenInclude(b => b.Users)
                .AnyAsync(oi => oi.OrderId == orderId && (oi.CampaignDetail.Campaign.OwnerId == userId ||
                                                          oi.CampaignDetail.Campaign.Business.OwnerId == userId ||
                                                          oi.CampaignDetail.Campaign.Business.Users.Any(u => u.Id == userId)));

            return canAccessOrder;
        }

        // Add any additional methods specific to OrderRepository here...
    }
}
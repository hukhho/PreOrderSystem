using PreorderPlatform.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreorderPlatform.Entity.Repositories.OrderItemRepositories
{
    public interface IOrderItemRepository : IRepositoryBase<OrderItem>
    {
        Task<OrderItem> GetOrderItemByIdAsync(int id);
    }
}

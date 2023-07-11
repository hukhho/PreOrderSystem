using PreorderPlatform.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreorderPlatform.Entity.Repositories.OrderRepositories
{
    public interface IOrderRepository : IRepositoryBase<Order>
    {
        Task<Order> GetOrderByIdAsync(int id);
    }
}

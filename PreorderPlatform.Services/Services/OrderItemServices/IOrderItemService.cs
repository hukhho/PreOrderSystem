using PreorderPlatform.Service.ViewModels.OrderItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreorderPlatform.Service.Services.OrderItemServices
{
    public interface IOrderItemService
    {
        Task<OrderItemViewModel> CreateOrderItemAsync(OrderItemCreateViewModel model);
        Task DeleteOrderItemAsync(int id);
        Task<OrderItemViewModel> GetOrderItemByIdAsync(int id);
        Task<List<OrderItemViewModel>> GetOrderItemsAsync();
        Task UpdateOrderItemAsync(OrderItemUpdateViewModel model);
    }
}

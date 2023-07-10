using PreorderPlatform.Entity.Models;
using PreorderPlatform.Service.Enum;
using PreorderPlatform.Service.Utility.Pagination;
using PreorderPlatform.Service.ViewModels.Order;
using PreorderPlatform.Service.ViewModels.Order.Request;
using PreorderPlatform.Service.ViewModels.Order.Response;
using PreorderPlatform.Service.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreorderPlatform.Service.Services.OrderServices
{
    public interface IOrderService
    {
        Task<OrderViewModel> CreateOrderAsync(OrderCreateViewModel model);
        Task DeleteOrderAsync(int id);
        Task<OrderViewModel> GetOrderByIdAsync(int id);
        Task<List<OrderViewModel>> GetOrdersAsync();
        Task UpdateOrderAsync(OrderUpdateViewModel model);

        Task<IList<OrderResponse>> GetAsync(PaginationParam<OrderEnum.OrderSort> paginationModel, OrderSearchRequest filterModel);
    }
}

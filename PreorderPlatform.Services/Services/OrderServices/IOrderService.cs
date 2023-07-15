using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PreorderPlatform.Entity.Models;
using PreorderPlatform.Service.Enum;
using PreorderPlatform.Service.Utility.Pagination;
using PreorderPlatform.Service.ViewModels.Order;
using PreorderPlatform.Service.ViewModels.Order.Request;
using PreorderPlatform.Service.ViewModels.Order.Response;
using PreorderPlatform.Service.ViewModels.User;

namespace PreorderPlatform.Service.Services.OrderServices
{
    public interface IOrderService
    {
        Task<OrderViewModel> CreateOrderAsync(OrderCreateViewModel model);

        Task DeleteOrderAsync(Guid orderId);

        Task<OrderByIdResponse> GetOrderByIdAsync(Guid orderId);

        Task<List<OrderViewModel>> GetOrdersAsync();

        Task UpdateOrderAsync(OrderUpdateViewModel model);

        /// <summary>
        /// Get a list of orders based on pagination and filter criteria.
        /// </summary>
        /// <param name="paginationModel">The pagination parameters.</param>
        /// <param name="filterModel">The filter criteria.</param>
        /// <returns>A tuple containing the list of orders and the total number of items.</returns>
        Task<(IList<OrderResponse> orders, int totalItems)>
        GetAsync(

                PaginationParam<OrderEnum.OrderSort> paginationModel,
                OrderSearchRequest filterModel

        );
    }
}

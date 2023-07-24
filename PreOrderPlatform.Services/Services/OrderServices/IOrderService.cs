using PreOrderPlatform.Service.Enums;
using PreOrderPlatform.Service.Utility.Pagination;
using PreOrderPlatform.Service.ViewModels.Order;
using PreOrderPlatform.Service.ViewModels.Order.Request;
using PreOrderPlatform.Service.ViewModels.Order.Response;

namespace PreOrderPlatform.Service.Services.OrderServices
{
    public interface IOrderService
    {
        Task<OrderOnCreatedResponse> CreateOrderAsync(Guid userId, OrderCreateViewModel model);

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

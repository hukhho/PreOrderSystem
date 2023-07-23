using PreOrderPlatform.Service.Enums;
using PreOrderPlatform.Service.Utility.Pagination;
using PreOrderPlatform.Service.ViewModels.OrderItem;

namespace PreOrderPlatform.Service.Services.OrderItemServices
{
    public interface IOrderItemService
    {
        Task<OrderItemViewModel> CreateOrderItemAsync(OrderItemCreateViewModel model);
        Task DeleteOrderItemAsync(Guid id);
        Task<(IList<OrderItemViewModel> orderItems, int totalItems)> GetAsync(PaginationParam<OrderItemEnum.OrderItemSort> paginationModel, OrderItemSearchRequest filterModel);
        Task<OrderItemViewModel> GetOrderItemByIdAsync(Guid id);
        Task<List<OrderItemViewModel>> GetOrderItemsAsync();
        Task UpdateOrderItemAsync(OrderItemUpdateViewModel model);
    }
}

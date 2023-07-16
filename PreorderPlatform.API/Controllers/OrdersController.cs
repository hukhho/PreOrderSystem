using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PreorderPlatform.Service.Services.OrderServices;
using PreorderPlatform.Service.ViewModels.Order;
using PreorderPlatform.Service.Exceptions;
using PreorderPlatform.Service.ViewModels.ApiResponse;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PreorderPlatform.Service.Utility.Pagination;
using PreorderPlatform.Service.Enum;
using PreorderPlatform.Service.ViewModels.Order.Request;
using PreorderPlatform.Service.ViewModels.Order.Response;
using System.Security.Claims;

namespace PreorderPlatform.API.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        //[HttpGet]
        //public async Task<IActionResult> GetAllOrders()
        //{
        //    try
        //    {
        //        var orders = await _orderService.GetOrdersAsync();
        //        return Ok(new ApiResponse<List<OrderViewModel>>(orders, "Orders fetched successfully.", true, null));
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError,
        //            new ApiResponse<object>(null, $"Error fetching orders: {ex.Message}", false, null));
        //    }
        //}
        [HttpGet]
        public async Task<IActionResult> GetAllOrders(
            [FromQuery] PaginationParam<OrderEnum.OrderSort> paginationModel,
            [FromQuery] OrderSearchRequest searchModel)
        {
            try
            {
                var start = DateTime.Now;
                var (orders, totalItems) = await _orderService.GetAsync(paginationModel, searchModel);
                Console.Write(DateTime.Now.Subtract(start).Milliseconds);

                return Ok(new ApiResponse<IList<OrderResponse>>(
                    orders,
                    "Orders fetched successfully.",
                    true,
                    new PaginationInfo(totalItems, paginationModel.PageSize, paginationModel.Page, (int)Math.Ceiling(totalItems / (double)paginationModel.PageSize))
                ));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponse<object>(null, $"Error fetching orders: {ex.Message}", false, null));
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(Guid id)
        {
            try
            {
                var order = await _orderService.GetOrderByIdAsync(id);
                return Ok(new ApiResponse<OrderByIdResponse>(order, "Order fetched successfully.", true, null));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ApiResponse<string>(null, ex.Message, false, null));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponse<object>(null, $"Error fetching order: {ex.Message}", false, null));
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderCreateViewModel model)
        {

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return Unauthorized(new ApiResponse<object>(null, "You don't have permission to access this resource.", false, null));
            }
            Guid userIdGuid;
            if (!Guid.TryParse(userId, out userIdGuid))
            {
                return BadRequest(
                    new ApiResponse<object>(null, "Invalid user ID format.", false, null)
                );
            }

            try
            {
                var order = await _orderService.CreateOrderAsync(userIdGuid, model);

                return StatusCode(StatusCodes.Status201Created, new ApiResponse<object>(order, "Order created successfully.", false, null));

                //return CreatedAtAction(nameof(GetOrderById),
                //                       new { id = order.Id },
                //                       new ApiResponse<OrderViewModel>(order, "Order created successfully.", true, null));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponse<object>(null, $"Error creating order: {ex.Message}", false, null));
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOrder(OrderUpdateViewModel model)
        {
            try
            {
                await _orderService.UpdateOrderAsync(model);
                return Ok(new ApiResponse<object>(null, "Order updated successfully.", true, null));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ApiResponse<object>(null, ex.Message, false, null));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponse<object>(null, $"Error updating order: {ex.Message}", false, null));
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            try
            {
                await _orderService.DeleteOrderAsync(id);
                return Ok(new ApiResponse<object>(null, "Order deleted successfully.", true, null));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ApiResponse<string>(null, ex.Message, false, null));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponse<object>(null, $"Error deleting order: {ex.Message}", false, null));
            }
        }
    }
}
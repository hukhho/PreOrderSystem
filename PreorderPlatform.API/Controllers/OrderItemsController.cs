using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PreorderPlatform.Service.Services.OrderItemServices;
using PreorderPlatform.Service.ViewModels.ApiResponse;
using PreorderPlatform.Service.ViewModels.OrderItem;
using PreorderPlatform.Service.Exceptions;
using PreorderPlatform.Service.Utility.Pagination;
using PreorderPlatform.Service.Enum;
using PreorderPlatform.Service.ViewModels.OrderItem.Response;

namespace PreorderPlatform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemsController : ControllerBase
    {
        private readonly IOrderItemService _orderItemService;
        private readonly IMapper _mapper;

        public OrderItemsController(IOrderItemService orderItemService, IMapper mapper)
        {
            _orderItemService = orderItemService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrderItems(
            [FromQuery] PaginationParam<OrderItemEnum.OrderItemSort> paginationModel,
            [FromQuery] OrderItemSearchRequest searchModel
        )
        {
            try
            {
                var start = DateTime.Now;
                var (orderItems, totalItems) = await _orderItemService.GetAsync(paginationModel, searchModel);
                Console.Write(DateTime.Now.Subtract(start).Milliseconds);

                return Ok(new ApiResponse<IList<OrderItemViewModel>>(
                    orderItems,
                    "Order items fetched successfully.",
                    true,
                    new PaginationInfo(totalItems, paginationModel.PageSize, paginationModel.Page, (int)Math.Ceiling(totalItems / (double)paginationModel.PageSize))
                ));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponse<object>(null, $"Error fetching order items: {ex.Message}", false, null));
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderItemById(int id)
        {
            try
            {
                var orderItem = await _orderItemService.GetOrderItemByIdAsync(id);
                return Ok(new ApiResponse<OrderItemByIdResponse>(orderItem, "Order item fetched successfully.", true, null));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ApiResponse<string>(null, ex.Message, false, null));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponse<object>(null, $"Error fetching order item: {ex.Message}", false, null));
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrderItem(OrderItemCreateViewModel model)
        {
            try
            {
                var orderItem = await _orderItemService.CreateOrderItemAsync(model);

                return CreatedAtAction(nameof(GetOrderItemById),
                                       new { id = orderItem.Id },
                                       new ApiResponse<OrderItemViewModel>(orderItem, "Order item created successfully.", true, null));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponse<object>(null, $"Error creating order item: {ex.Message}", false, null));
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOrderItem(OrderItemUpdateViewModel model)
        {
            try
            {
                await _orderItemService.UpdateOrderItemAsync(model);
                return Ok(new ApiResponse<object>(null, "Order item updated successfully.", true, null));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ApiResponse<object>(null, ex.Message, false, null));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponse<object>(null, $"Error updating order item: {ex.Message}", false, null));
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderItem(int id)
        {
            try
            {
                await _orderItemService.DeleteOrderItemAsync(id);
                return Ok(new ApiResponse<object>(null, "Order item deleted successfully.", true, null));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ApiResponse<string>(null, ex.Message, false, null));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponse<object>(null, $"Error deleting order item: {ex.Message}", false, null));
            }
        }
    }
}
﻿//using AutoMapper;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using PreOrderPlatform.Service.Enums;
//using PreOrderPlatform.Service.Services.Exceptions;
//using PreOrderPlatform.Service.Services.OrderItemServices;
//using PreOrderPlatform.Service.Services.OrderServices;
//using PreOrderPlatform.Service.Utility.Pagination;
//using PreOrderPlatform.Service.ViewModels.ApiResponse;
//using PreOrderPlatform.Service.ViewModels.OrderItem;

//namespace PreOrderPlatform.API.Controllers
//{
//    [Route("api/orders/{orderId}/orderitems")]
//    [ApiController]
//    [Authorize(Policy = "MustBeOrderAccess")]
//    public class OrderItemsController : ControllerBase
//    {
//        private readonly IOrderItemService _orderItemService;
//        private readonly IOrderService _orderService;
//        private readonly IMapper _mapper;

//        public OrderItemsController(IOrderItemService orderItemService, IOrderService orderService, IMapper mapper)
//        {
//            _orderItemService = orderItemService;
//            _orderService = orderService;
//            _mapper = mapper;
//        }

//        [HttpGet]
//        public async Task<IActionResult> GetAllOrderItems(
//            [FromRoute] Guid orderId,
//            [FromQuery] PaginationParam<OrderItemEnum.OrderItemSort> paginationModel,
//            [FromQuery] OrderItemSearchRequest searchModel
//        )
//        {
//            try
//            {
//                searchModel.OrderId = orderId;
//                var start = DateTime.Now;
//                var (orderItems, totalItems) = await _orderItemService.GetAsync(paginationModel, searchModel);
//                Console.Write(DateTime.Now.Subtract(start).Milliseconds);

//                return Ok(new ApiResponse<IList<OrderItemViewModel>>(
//                    orderItems,
//                    "Order items fetched successfully.",
//                    true,
//                    new PaginationInfo(totalItems, paginationModel.PageSize, paginationModel.Page, (int)Math.Ceiling(totalItems / (double)paginationModel.PageSize))
//                ));
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError,
//                    new ApiResponse<object>(null, $"Error fetching order items: {ex.Message}", false, null));
//            }
//        }

//        [HttpGet("{id}")]
//        public async Task<IActionResult> GetOrderItemById(
//            [FromRoute] Guid orderId,
//            Guid id)
//        {
//            try
//            {
//                var order = await _orderService.GetOrderByIdAsync(orderId);

//                var orderItem = await _orderItemService.GetOrderItemByIdAsync(id);

//                if (orderItem.OrderId != order.Id)
//                    throw new NotFoundException($"Order item not found in order id {orderId}.");

//                return Ok(new ApiResponse<OrderItemViewModel>(orderItem, "Order item fetched successfully.", true, null));
//            }
//            catch (NotFoundException ex)
//            {
//                return NotFound(new ApiResponse<string>(null, ex.Message, false, null));
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError,
//                    new ApiResponse<object>(null, $"Error fetching order item: {ex.Message}", false, null));
//            }
//        }

//        //[HttpPost]
//        //public async Task<IActionResult> CreateOrderItem(OrderItemCreateViewModel model)
//        //{
//        //    try
//        //    {
//        //        var orderItem = await _orderItemService.CreateOrderItemAsync(model);

//        //        return CreatedAtAction(nameof(GetOrderItemById),
//        //                               new { id = orderItem.Id },
//        //                               new ApiResponse<OrderItemViewModel>(orderItem, "Order item created successfully.", true, null));
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        return StatusCode(StatusCodes.Status500InternalServerError,
//        //            new ApiResponse<object>(null, $"Error creating order item: {ex.Message}", false, null));
//        //    }
//        //}

//        //[HttpPut]
//        //public async Task<IActionResult> UpdateOrderItem(OrderItemUpdateViewModel model)
//        //{
//        //    try
//        //    {
//        //        await _orderItemService.UpdateOrderItemAsync(model);
//        //        return Ok(new ApiResponse<object>(null, "Order item updated successfully.", true, null));
//        //    }
//        //    catch (NotFoundException ex)
//        //    {
//        //        return NotFound(new ApiResponse<object>(null, ex.Message, false, null));
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        return StatusCode(StatusCodes.Status500InternalServerError,
//        //            new ApiResponse<object>(null, $"Error updating order item: {ex.Message}", false, null));
//        //    }
//        //}

//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteOrderItem(Guid id)
//        {
//            try
//            {
//                await _orderItemService.DeleteOrderItemAsync(id);
//                return Ok(new ApiResponse<object>(null, "Order item deleted successfully.", true, null));
//            }
//            catch (NotFoundException ex)
//            {
//                return NotFound(new ApiResponse<string>(null, ex.Message, false, null));
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError,
//                    new ApiResponse<object>(null, $"Error deleting order item: {ex.Message}", false, null));
//            }
//        }
//    }
//}
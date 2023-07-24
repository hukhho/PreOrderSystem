using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PreOrderPlatform.Service.Enums;
using PreOrderPlatform.Service.Services.Exceptions;
using PreOrderPlatform.Service.Services.OrderServices;
using PreOrderPlatform.Service.Services.PaymentServices;
using PreOrderPlatform.Service.Services.UserServices;
using PreOrderPlatform.Service.Utility.Pagination;
using PreOrderPlatform.Service.ViewModels.ApiResponse;
using PreOrderPlatform.Service.ViewModels.Order;
using PreOrderPlatform.Service.ViewModels.Order.Request;
using PreOrderPlatform.Service.ViewModels.Order.Response;
using PreOrderPlatform.Service.ViewModels.Payment;

namespace PreOrderPlatform.API.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IPaymentService _paymentService;

        public OrdersController(IOrderService orderService, IUserService userService, IMapper mapper, IPaymentService paymentService)
        {
            _orderService = orderService;
            _userService = userService;
            _mapper = mapper;
            _paymentService = paymentService;
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
                var roleName = User.FindFirst(ClaimTypes.Role)?.Value;
                if (roleName == null)
                    throw new NotFoundException("User role not found.");

                if (roleName.Equals(UserRole.CUSTOMER.ToString()))
                {
                    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    searchModel.UserId = Guid.Parse(userId);
                }

                if (roleName.Equals(UserRole.BUSINESS_OWNER.ToString()) || roleName.Equals(UserRole.BUSINESS_STAFF.ToString()))
                {
                    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    var user = await _userService.GetUserByIdAsync(Guid.Parse(userId));
                    if (user.BusinessId == null)
                        throw new NotFoundException("Business owner or staff not in any business.");
                    searchModel.BusinessId = user.BusinessId;
                }

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
        [Authorize(Policy = "MustBeOrderAccess")]

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
          
            OrderOnCreatedResponse order = await _orderService.CreateOrderAsync(userIdGuid, model);
            var businessPayment = order.businessPaymentCredential;
            MomoPaymentCreateViewModel momoPaymentCreateViewModel = new MomoPaymentCreateViewModel();
            momoPaymentCreateViewModel.amount = order.RequiredDepositAmount.ToString();
            momoPaymentCreateViewModel.orderId = order.Id.ToString();  
            momoPaymentCreateViewModel.orderInfo = order.RevicerPhone.ToString();
            momoPaymentCreateViewModel.redirectUrl = "https://localhost:5019/api/orders/momo-payment-callback";
            momoPaymentCreateViewModel.inputUrl = "https://localhost:5019/";
            momoPaymentCreateViewModel.requestId = Guid.NewGuid().ToString();
            momoPaymentCreateViewModel.requestType = "captureMoMoWallet";
            momoPaymentCreateViewModel.accessKey = businessPayment.MomoAccessToken;
            momoPaymentCreateViewModel.partnerCode = businessPayment.MomoPartnerCode;
            momoPaymentCreateViewModel.serectkey = businessPayment.MomoSecretToken;


            var payment = await _paymentService.CreateMomoPayment(momoPaymentCreateViewModel);

            order.paymentUrl = payment;

            return StatusCode(StatusCodes.Status201Created, new ApiResponse<OrderOnCreatedResponse>(order, "Order created successfully.", false, null));

                //return CreatedAtAction(nameof(GetOrderById),
                //                       new { id = order.Id },
                //                       new ApiResponse<OrderViewModel>(order, "Order created successfully.", true, null));
          
        }

        [HttpPut]
        [Authorize(Policy = "MustBeOrderAccess")]
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

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteOrder(Guid id)
        //{
        //    try
        //    {
        //        await _orderService.DeleteOrderAsync(id);
        //        return Ok(new ApiResponse<object>(null, "Order deleted successfully.", true, null));
        //    }
        //    catch (NotFoundException ex)
        //    {
        //        return NotFound(new ApiResponse<string>(null, ex.Message, false, null));
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError,
        //            new ApiResponse<object>(null, $"Error deleting order: {ex.Message}", false, null));
        //    }
        //}
    }
}
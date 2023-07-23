using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PreOrderPlatform.Service.Enums;
using PreOrderPlatform.Service.Services.Exceptions;
using PreOrderPlatform.Service.Services.PaymentServices;
using PreOrderPlatform.Service.Utility.CustomAuthorizeAttribute;
using PreOrderPlatform.Service.Utility.Pagination;
using PreOrderPlatform.Service.ViewModels.ApiResponse;
using PreOrderPlatform.Service.ViewModels.Payment;

namespace PreOrderPlatform.API.Controllers
{
    [Route("api/payments")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        private readonly ILogger<PaymentsController> _logger;
        public PaymentsController(IPaymentService paymentService, ILogger<PaymentsController> logger)
        {
            _paymentService = paymentService;
            _logger = logger;
        }



        [HttpGet("/test-momo")]
        public async Task<IActionResult> TestMomo(

        )
        {
            try
            {
                var start = DateTime.Now;
                var momoResponseJson = await _paymentService.TestMomo();

                Console.Write(DateTime.Now.Subtract(start).Milliseconds);

                return Ok(new ApiResponse<Object>(
                    momoResponseJson,
                    "Momo test.",
                    true,
                    null
                ));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = $"Error fetching payments: {ex.Message}" });
            }
        }


        [HttpGet("/test-vnpay")]
        public async Task<IActionResult> TestVnPay(

       )
        {
            try
            {
                var start = DateTime.Now;
                var vnPayUrlLink = await _paymentService.TestVNPay();

                Console.Write(DateTime.Now.Subtract(start).Milliseconds);

                return Ok(new ApiResponse<object>(
                    new Dictionary<string, object> { { "vnpayRedirectURL", vnPayUrlLink } },
                    "VnPay test.",
                    true,
                    null
                ));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = $"Error fetching payments: {ex.Message}" });
            }
        }

        [HttpGet]
        //This allows users to see an overview of all payments they have made
        public async Task<IActionResult> GetPayments(
            [FromQuery] PaginationParam<PaymentEnum.PaymentSort> paginationModel,
            [FromQuery] PaymentSearchRequest searchModel
        )
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;            
                searchModel.UserId = Guid.Parse(userId);

                var start = DateTime.Now;
                var (payments, totalItems) = await _paymentService.GetAsync(paginationModel, searchModel);
                Console.Write(DateTime.Now.Subtract(start).Milliseconds);

                return Ok(new ApiResponse<IList<PaymentViewModel>>(
                    payments,
                    "Payments fetched successfully.",
                    true,
                    new PaginationInfo(totalItems, paginationModel.PageSize, paginationModel.Page, (int)Math.Ceiling(totalItems / (double)paginationModel.PageSize))
                ));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = $"Error fetching payments: {ex.Message}" });
            }
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "MustBePaymentAccess")]
        public async Task<IActionResult> GetPayment(Guid id)
        {
            try
            {
                var payment = await _paymentService.GetPaymentByIdAsync(id);
                return Ok(payment);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = $"Error fetching payment: {ex.Message}" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreatePayment([FromBody] PaymentCreateViewModel model)
        {
            try
            {
                var payment = await _paymentService.CreatePaymentAsync(model);
                return CreatedAtAction(nameof(GetPayment), new { id = payment.Id }, payment);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = $"Error creating payment: {ex.Message}" });
            }
        }

        [HttpPut("{id}")]
        [CustomAuthorize(Roles = "BUSSINESS_OWNER,BUSINESS_STAFF")]
        [Authorize(Policy = "MustBePaymentAccess")]
        public async Task<IActionResult> UpdatePayment(Guid id, [FromBody] PaymentUpdateViewModel model)
        {
            if (id != model.Id)
            {
                return BadRequest("The ID in the URL and the model do not match.");
            }

            try
            {
                await _paymentService.UpdatePaymentAsync(model);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = $"Error updating payment: {ex.Message}" });
            }
        }

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeletePayment(Guid id)
        //{
        //    try
        //    {
        //        await _paymentService.DeletePaymentAsync(id);
        //        return NoContent();
        //    }
        //    catch (NotFoundException ex)
        //    {
        //        return NotFound(new { message = ex.Message });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError,
        //            new { message = $"Error deleting payment: {ex.Message}" });
        //    }
        //}




    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PreorderPlatform.Service.Services.PaymentServices;
using PreorderPlatform.Service.ViewModels.Payment;
using PreorderPlatform.Service.Exceptions;
using PreorderPlatform.Service.Utility.Pagination;
using PreorderPlatform.Service.Enum;
using PreorderPlatform.Service.ViewModels.ApiResponse;

namespace PreorderPlatform.API.Controllers
{
    [Route("api/payments")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPayments(
            [FromQuery] PaginationParam<PaymentEnum.PaymentSort> paginationModel,
            [FromQuery] PaymentSearchRequest searchModel
        )
        {
            try
            {
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
        public async Task<IActionResult> GetPayment(int id)
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
        public async Task<IActionResult> UpdatePayment(int id, [FromBody] PaymentUpdateViewModel model)
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            try
            {
                await _paymentService.DeletePaymentAsync(id);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = $"Error deleting payment: {ex.Message}" });
            }
        }
    }
}
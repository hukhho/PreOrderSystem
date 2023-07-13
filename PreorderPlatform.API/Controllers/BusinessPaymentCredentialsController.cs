using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PreorderPlatform.Service.ViewModels.ApiResponse;
using PreorderPlatform.Service.Exceptions;
using PreorderPlatform.Service.ViewModels.BusinessPaymentCredential;
using PreorderPlatform.Service.Services.BusinessPaymentCredentialServices;
using PreorderPlatform.Service.Utility.Pagination;
using PreorderPlatform.Services.Enum;
using PreorderPlatform.Service.Utility.CustomAuthorizeAttribute;
using System.Security.Claims;

namespace PreorderPlatform.API.Controllers
{
    [Route("api/business/business-payment-credentials")]
    [ApiController]
    public class BusinessPaymentCredentialsController : ControllerBase
    {
        private readonly IBusinessPaymentCredentialService _businessPaymentCredentialService;
        private readonly IMapper _mapper;

        public BusinessPaymentCredentialsController(IBusinessPaymentCredentialService businessPaymentCredentialService, IMapper mapper)
        {
            _businessPaymentCredentialService = businessPaymentCredentialService;
            _mapper = mapper;
        }

        [HttpGet]
        [CustomAuthorize(Roles = "ADMIN,BUSINESS_OWNER")]
        public async Task<IActionResult> GetAllBusinessPaymentCredentials(
            [FromQuery] PaginationParam<BusinessPaymentCredentialEnum.BusinessPaymentCredentialSort> paginationModel,
            [FromQuery] BusinessPaymentCredentialSearchRequest searchModel
        )
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized(new ApiResponse<object>(null, "You don't have permission to access this resource.", false, null));
            }
            var roleName = User.FindFirst(ClaimTypes.Role)?.Value;

            if (roleName == null)
            {
                return Unauthorized(new ApiResponse<object>(null, "You don't have permission to access this resource.", false, null));
            }

            if (roleName != "ADMIN")
            {
                int userIdInt;
                if (!int.TryParse(userId, out userIdInt))
                {
                    return BadRequest(new ApiResponse<object>(null, "Invalid user ID format.", false, null));
                }
                var business = await _businessPaymentCredentialService.GetBusinessByOwnerIdAsync(userIdInt);
                int businessId = business.Id;
                searchModel.BusinessId = businessId;
            }
            try
            {
                var start = DateTime.Now;
                var (businessPaymentCredentials, totalItems) = await _businessPaymentCredentialService.GetAsync(paginationModel, searchModel);
                Console.Write(DateTime.Now.Subtract(start).Milliseconds);

                return Ok(new ApiResponse<IList<BusinessPaymentCredentialViewModel>>(
                    businessPaymentCredentials,
                    "Business payment credentials fetched successfully.",
                    true,
                    new PaginationInfo(totalItems, paginationModel.PageSize, paginationModel.Page, (int)Math.Ceiling(totalItems / (double)paginationModel.PageSize))
                ));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponse<object>(null, $"Error fetching business payment credentials: {ex.Message}", false, null));
            }
        }

        [HttpGet("{id}")]
        [CustomAuthorize(Roles = "ADMIN,BUSINESS_OWNER")]
        public async Task<IActionResult> GetBusinessPaymentCredentialsById(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized(new ApiResponse<object>(null, "You don't have permission to access this resource.", false, null));
            }
            try
            {
                var businessPaymentCredentials = await _businessPaymentCredentialService.GetBusinessPaymentCredentialByIdAsync(id, userId);
                return Ok(new ApiResponse<BusinessPaymentCredentialViewModel>(businessPaymentCredentials, "Business payment credentials fetched successfully.", true, null));
            }
            catch (ArgumentException ex)
            {
                return StatusCode(StatusCodes.Status403Forbidden, new ApiResponse<object>(null, ex.Message, false, null));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ApiResponse<string>(null, ex.Message, false, null));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponse<object>(null, $"Error fetching business payment credentials: {ex.Message}", false, null));
            }
        }

      
        [HttpPost]
        public async Task<IActionResult> CreateBusinessPaymentCredentials(BusinessPaymentCredentialCreateViewModel model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized(new ApiResponse<object>(null, "You don't have permission to access this resource.", false, null));
            }
            int userIdInt;
            if (!int.TryParse(userId, out userIdInt))
            {
                return BadRequest(new ApiResponse<object>(null, "Invalid user ID format.", false, null));
            }


            var business = await _businessPaymentCredentialService.GetBusinessByOwnerIdAsync(userIdInt);
            int businessId = business.Id;

            var businessPaymentCredentialList = await _businessPaymentCredentialService.GetBusinessPaymentCredentialsAsync();

            var filteredList = businessPaymentCredentialList
                     .Where(c => c.BusinessId == businessId)
                     .ToList();

            var hasMain =  filteredList.Any(c => c.IsMain == true);


            try
            {
                var businessPaymentCredentials = await _businessPaymentCredentialService.CreateBusinessPaymentCredentialAsync(model);

                return CreatedAtAction(nameof(GetBusinessPaymentCredentialsById),
                new { id = businessPaymentCredentials.Id },
                                       new ApiResponse<BusinessPaymentCredentialViewModel>(businessPaymentCredentials, "Business payment credentials created successfully.", true, null));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponse<object>(null, $"Error creating business payment credentials: {ex.Message}", false, null));
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBusinessPaymentCredentials(BusinessPaymentCredentialUpdateViewModel model)
        {
            try
            {
                await _businessPaymentCredentialService.UpdateBusinessPaymentCredentialAsync(model);
                return Ok(new ApiResponse<object>(null, "Business payment credentials updated successfully.", true, null));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ApiResponse<object>(null, ex.Message, false, null));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponse<object>(null, $"Error updating business payment credentials: {ex.Message}", false, null));
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBusinessPaymentCredentials(int id)
        {
            try
            {
                await _businessPaymentCredentialService.DeleteBusinessPaymentCredentialAsync(id);
                return Ok(new ApiResponse<object>(null, "Business payment credentials deleted successfully.", true, null));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ApiResponse<string>(null, ex.Message, false, null));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponse<object>(null, $"Error deleting business payment credentials: {ex.Message}", false, null));
            }
        }
    }
}
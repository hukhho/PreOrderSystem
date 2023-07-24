using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PreOrderPlatform.Service.Enums;
using PreOrderPlatform.Service.Services.BusinessPaymentCredentialServices;
using PreOrderPlatform.Service.Services.Exceptions;
using PreOrderPlatform.Service.Utility.Pagination;
using PreOrderPlatform.Service.ViewModels.ApiResponse;
using PreOrderPlatform.Service.ViewModels.BusinessPaymentCredential;

namespace PreOrderPlatform.API.Controllers
{
    [Route("api/business/{businessId}/business-payment-credentials")]
    [ApiController]
    public class BusinessPaymentCredentialsController : ControllerBase
    {
        private readonly IBusinessPaymentCredentialService _businessPaymentCredentialService;
        private readonly IMapper _mapper;

        public BusinessPaymentCredentialsController(
            IBusinessPaymentCredentialService businessPaymentCredentialService,
            IMapper mapper
        )
        {
            _businessPaymentCredentialService = businessPaymentCredentialService;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Policy = "MustBeBusinessOwner")]
        public async Task<IActionResult> GetAllBusinessPaymentCredentials(
            [FromRoute] Guid businessId,
            [FromQuery] PaginationParam<BusinessPaymentCredentialEnum.BusinessPaymentCredentialSort> paginationModel,
            [FromQuery] BusinessPaymentCredentialSearchRequest searchModel
        )
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized(
                    new ApiResponse<object>(
                        null,
                        "You don't have permission to access this resource.",
                        false,
                        null
                    )
                );
            }

            var roleName = User.FindFirst(ClaimTypes.Role)?.Value;

            if (roleName == null)
            {
                return Unauthorized(
                    new ApiResponse<object>(
                        null,
                        "You don't have permission to access this resource.",
                        false,
                        null
                    )
                );
            }

            searchModel.BusinessId = businessId;


            try
            {
                var start = DateTime.Now;
                var (businessPaymentCredentials, totalItems) =
                    await _businessPaymentCredentialService.GetAsync(paginationModel, searchModel);
                Console.Write(DateTime.Now.Subtract(start).Milliseconds);

                return Ok(
                    new ApiResponse<IList<BusinessPaymentCredentialViewModel>>(
                        businessPaymentCredentials,
                        "Business payment credentials fetched successfully.",
                        true,
                        new PaginationInfo(
                            totalItems,
                            paginationModel.PageSize,
                            paginationModel.Page,
                            (int)Math.Ceiling(totalItems / (double)paginationModel.PageSize)
                        )
                    )
                );
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new ApiResponse<object>(
                        null,
                        $"Error fetching business payment credentials: {ex.Message}",
                        false,
                        null
                    )
                );
            }
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "MustBeBusinessPaymentCredentialOwner")]
        public async Task<IActionResult> GetBusinessPaymentCredentialsById(Guid id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized(
                    new ApiResponse<object>(
                        null,
                        "You don't have permission to access this resource.",
                        false,
                        null
                    )
                );
            }

            try
            {
                var businessPaymentCredentials =
                    await _businessPaymentCredentialService.GetBusinessPaymentCredentialByIdAsync(
                        id
                    );
                return Ok(
                    new ApiResponse<BusinessPaymentCredentialViewModel>(
                        businessPaymentCredentials,
                        "Business payment credentials fetched successfully.",
                        true,
                        null
                    )
                );
            }
            catch (ArgumentException ex)
            {
                return StatusCode(
                    StatusCodes.Status403Forbidden,
                    new ApiResponse<object>(null, ex.Message, false, null)
                );
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ApiResponse<string>(null, ex.Message, false, null));
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new ApiResponse<object>(
                        null,
                        $"Error fetching business payment credentials: {ex.Message}",
                        false,
                        null
                    )
                );
            }
        }

        [HttpPost]
        [Authorize(Policy = "MustBeBusinessOwner")]
        public async Task<IActionResult> CreateBusinessPaymentCredentials(
            [FromRoute] Guid businessId,
            BusinessPaymentCredentialCreateViewModel model
        )
        {
            var businessPaymentCredentialList =
                await _businessPaymentCredentialService.GetBusinessPaymentCredentialsAsync();

            var filteredList = businessPaymentCredentialList
                .Where(c => c.BusinessId == businessId)
                .ToList();

            bool hasMain = filteredList.Any(c => c.IsMain == true);

            model.BusinessId = businessId;
            model.IsMain = !hasMain;
            model.CreateAt = DateTime.Now;
            model.Status = true;

            try
            {
                var businessPaymentCredentials =
                    await _businessPaymentCredentialService.CreateBusinessPaymentCredentialAsync(
                        model
                    );


                return StatusCode(StatusCodes.Status201Created,
                    new ApiResponse<object>(businessPaymentCredentials, "Create business successly!", false, null));
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new ApiResponse<object>(
                        null,
                        $"Error creating business payment credentials: {ex.Message}",
                        false,
                        null
                    )
                );
            }
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "MustBeBusinessPaymentCredentialOwner")]
        public async Task<IActionResult> UpdateBusinessPaymentCredentials(
            Guid id,
            BusinessPaymentCredentialUpdateViewModel model
        )
        {
            try
            {
                Console.WriteLine($"UpdateBusinessPaymentCredentials id: {id} ");
                await _businessPaymentCredentialService.UpdateBusinessPaymentCredentialAsync(id, model);

                return Ok(
                    new ApiResponse<object>(
                        null,
                        "Business payment credentials updated successfully.",
                        true,
                        null
                    )
                );
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ApiResponse<object>(null, ex.Message, false, null));
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new ApiResponse<object>(
                        null,
                        $"Error updating business payment credentials: {ex.Message}",
                        false,
                        null
                    )
                );
            }
        }

        [HttpPut("set-main/{id}")]
        [Authorize(Policy = "MustBeBusinessPaymentCredentialOwner")]
        public async Task<IActionResult> SetMainBusinessPaymentCredentials(
            Guid id
        )
        {
            try
            {
                Console.WriteLine($"SetMainBusinessPaymentCredentials id: {id} ");
                await _businessPaymentCredentialService.SetMainBusinessPaymentCredentialAsync(id);

                return Ok(
                    new ApiResponse<object>(
                        null,
                        "Business payment credentials set main successfully.",
                        true,
                        null
                    )
                );
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ApiResponse<object>(null, ex.Message, false, null));
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new ApiResponse<object>(
                        null,
                        $"Error updating business payment credentials: {ex.Message}",
                        false,
                        null
                    )
                );
            }
        }



        [HttpDelete("{id}")]
        [Authorize(Policy = "MustBeBusinessPaymentCredentialOwner")]
        public async Task<IActionResult> DeleteBusinessPaymentCredentials(Guid id)
        {
            try
            {
                var businessPaymentCredentials =
                    await _businessPaymentCredentialService.GetBusinessPaymentCredentialByIdAsync(
                        id
                    );
                if (businessPaymentCredentials.IsMain == null)
                {
                    return BadRequest(
                        new ApiResponse<object>(
                            null,
                            "Business payment credentials have error occur.",
                            false,
                            null
                        )
                    );
                }

                if ((bool) businessPaymentCredentials.IsMain)
                {
                    return BadRequest(
                        new ApiResponse<object>(
                            null,
                            "Can't delete main business payment credentials.",
                            false,
                            null
                        )
                    );
                }


                await _businessPaymentCredentialService.DeleteBusinessPaymentCredentialAsync(id);
                return Ok(
                    new ApiResponse<object>(
                        null,
                        "Business payment credentials deleted successfully.",
                        true,
                        null
                    )
                );
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ApiResponse<string>(null, ex.Message, false, null));
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new ApiResponse<object>(
                        null,
                        $"Error deleting business payment credentials: {ex.Message}",
                        false,
                        null
                    )
                );
            }
        }
    }
}
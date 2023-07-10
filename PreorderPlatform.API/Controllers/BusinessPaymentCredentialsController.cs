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

namespace PreorderPlatform.API.Controllers
{
    [Route("api/[controller]")]
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
        public async Task<IActionResult> GetAllBusinessPaymentCredentials()
        {
            try
            {
                var businessPaymentCredentialsList = await _businessPaymentCredentialService.GetBusinessPaymentCredentialsAsync();
                return Ok(new ApiResponse<List<BusinessPaymentCredentialViewModel>>(businessPaymentCredentialsList, "Business payment credentials fetched successfully.", true, null));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponse<object>(null, $"Error fetching business payment credentials: {ex.Message}", false, null));
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBusinessPaymentCredentialsById(int id)
        {
            try
            {
                var businessPaymentCredentials = await _businessPaymentCredentialService.GetBusinessPaymentCredentialByIdAsync(id);
                return Ok(new ApiResponse<BusinessPaymentCredentialViewModel>(businessPaymentCredentials, "Business payment credentials fetched successfully.", true, null));
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
﻿using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PreOrderPlatform.Service.Enums;
using PreOrderPlatform.Service.Services.BusinessServices;
using PreOrderPlatform.Service.Services.Exceptions;
using PreOrderPlatform.Service.Utility.CustomAuthorizeAttribute;
using PreOrderPlatform.Service.Utility.Pagination;
using PreOrderPlatform.Service.ViewModels.ApiResponse;
using PreOrderPlatform.Service.ViewModels.Business.Request;
using PreOrderPlatform.Service.ViewModels.Business.Response;

namespace PreOrderPlatform.API.Controllers
{
    [Route("api/business")]
    [ApiController]
    public class BusinessesController : ControllerBase
    {
        private readonly IBusinessService _businessService;
        private readonly IMapper _mapper;

        public BusinessesController(IBusinessService businessService, IMapper mapper)
        {
            _businessService = businessService;
            _mapper = mapper;
        }

        [HttpGet]
        [CustomAuthorize(Roles = "ADMIN,BUSINESS_OWNER")]
        public async Task<IActionResult> GetAllBusinesses(
            [FromQuery] PaginationParam<BusinessEnum.BusinessSort> paginationModel,
            [FromQuery] BusinessSearchRequest searchModel
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
                Guid ownerId;
                if (Guid.TryParse(userId, out ownerId))
                {
                    searchModel.OwnerId = ownerId;
                }
            }
            try
            {
                var start = DateTime.Now;
                var (businesses, totalItems) = await _businessService.GetAsync(paginationModel, searchModel);
                Console.Write(DateTime.Now.Subtract(start).Milliseconds);

                return Ok(new ApiResponse<IList<BusinessResponse>>(
                    businesses,
                    "Businesses fetched successfully.",
                    true,
                    new PaginationInfo(totalItems, paginationModel.PageSize, paginationModel.Page, (int)Math.Ceiling(totalItems / (double)paginationModel.PageSize))
                ));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponse<object>(null, $"Error fetching businesses: {ex.Message}", false, null));
            }
        }


        [HttpGet("{id}")]
        [Authorize(Policy = "MustBeBusinessOwner")]
        public async Task<IActionResult> GetBusinessById(Guid id)
        {

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized(new ApiResponse<object>(null, "You don't have permission to access this resource.", false, null));
            }
            try
            {
                var business = await _businessService.GetBusinessByIdAsync(id);
                return Ok(new ApiResponse<BusinessByIdResponse>(business, "Business fetched successfully.", true, null));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ApiResponse<string>(null, ex.Message, false, null));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponse<object>(null, $"Error fetching business: {ex.Message}", false, null));
            }
        }

        [HttpPost]
        [CustomAuthorize(Roles = "ADMIN,BUSINESS_OWNER")]
        public async Task<IActionResult> CreateBusiness([FromBody] BusinessCreateRequest model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized(new ApiResponse<object>(null, "You don't have permission to access this resource.", false, null));
            }
            try
            {
                Guid userIdInt;
                if (!Guid.TryParse(userId, out userIdInt))
                {
                    return BadRequest(new ApiResponse<object>(null, "Invalid user ID.", false, null));
                }

                model.OwnerId = userIdInt;
                model.Status = true;

                Console.WriteLine($"modeL {model}");

                var businessResponse = await _businessService.CreateBusinessAsync(model);
                return StatusCode(StatusCodes.Status201Created, new ApiResponse<object>(null, "Create business successly!", false, null));
            }
            catch (ServiceException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<object>(null, ex.Message, false, null));
            }
            catch (Exception ex)
            {
                // Handle other exceptions and return a generic error message
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<object>(null, "An unexpected error occurred.", false, null));
            }
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "MustBeBusinessOwner")]
        public async Task<IActionResult> UpdateBusiness(Guid id, [FromBody] BusinessUpdateRequest updateRequest)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized(new ApiResponse<object>(null, "You don't have permission to access this resource.", false, null));
            }

            try
            {
                var businessByIdResponse = await _businessService.GetBusinessByIdAsync(id);

                if (businessByIdResponse == null)
                {
                    return NotFound(new ApiResponse<object>(null, $"Business with ID {id} not found.", false, null));
                }
                updateRequest.Id = id;
                // Update the business with the changes
                await _businessService.UpdateBusinessAsync(updateRequest);

                return Ok(new ApiResponse<object>(null, "Business updated successfully.", true, null));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ApiResponse<object>(null, ex.Message, false, null));
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new ApiResponse<object>(null, ex.Message, false, null));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponse<object>(null, $"Error updating business: {ex.Message}", false, null));
            }
        }

        //[HttpDelete("{id}")]
        //[CustomAuthorize(Roles = "ADMIN,BUSINESS_OWNER")]
        //public async Task<IActionResult> DeleteBusiness(int id)
        //{
        //    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        //    if (userId == null)
        //    {
        //        return Unauthorized(new ApiResponse<object>(null, "You don't have permission to access this resource.", false, null));
        //    }
        //    try
        //    {
        //        var businessByIdResponse = await _businessService.GetBusinessByIdAsync(id, userId);

        //        if (businessByIdResponse == null)
        //        {
        //            return NotFound(new ApiResponse<object>(null, $"Business with ID {id} not found.", false, null));
        //        }

        //        await _businessService.DeleteBusinessAsync(id);
        //        return Ok(new ApiResponse<object>(null, "Business deleted successfully.", true, null));
        //    }
        //    catch (NotFoundException ex)
        //    {
        //        return NotFound(new ApiResponse<string>(null, ex.Message, false, null));
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError,
        //            new ApiResponse<object>(null, $"Error deleting business: {ex.Message}", false, null));
        //    }
        //}

        [HttpPatch("{id}")]
        [Authorize(Policy = "MustBeBusinessOwner")]
        public async Task<IActionResult> UpdateBusiness(Guid id, [FromBody] JsonPatchDocument<BusinessPatchRequest> patchDocument)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized(new ApiResponse<object>(null, "You don't have permission to access this resource.", false, null));
            }

            try
            {
                var businessByIdResponse = await _businessService.GetBusinessByIdAsync(id);

                if (businessByIdResponse == null)
                {
                    return NotFound(new ApiResponse<object>(null, $"Business with ID {id} not found.", false, null));
                }

                var businessUpdateRequest = _mapper.Map<BusinessPatchRequest>(businessByIdResponse);
                patchDocument.ApplyTo(businessUpdateRequest);
                // Update the business status
                businessUpdateRequest.Id = id;

                await _businessService.UpdateBusinessAsync(_mapper.Map<BusinessUpdateRequest>(businessUpdateRequest));

                return Ok(new ApiResponse<object>(null, "Business status updated successfully.", true, null));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ApiResponse<object>(null, ex.Message, false, null));
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new ApiResponse<object>(null, ex.Message, false, null));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponse<object>(null, $"Error updating business status: {ex.Message}", false, null));
            }
        }
    }


}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PreorderPlatform.Service.ViewModels.ApiResponse;
using PreorderPlatform.Service.Exceptions;
using PreorderPlatform.Service.Services.CampaignDetailServices;
using PreorderPlatform.Service.ViewModels.CampaignPrice.Request;
using PreorderPlatform.Service.ViewModels.CampaignPrice.Response;
using PreorderPlatform.Service.Utility.Pagination;
using PreorderPlatform.Service.Enum;
using PreorderPlatform.Service.ViewModels.Campaign.Response;

namespace PreorderPlatform.API.Controllers
{
    [Route("api/campaign/campaign-details")]
    [ApiController]
    public class CampaignDetailsController : ControllerBase
    {
        private readonly ICampaignDetailService _campaignDetailService;
        private readonly IMapper _mapper;

        public CampaignDetailsController(ICampaignDetailService campaignDetailsService, IMapper mapper)
        {
            _campaignDetailService = campaignDetailsService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCampaignDetails(
            [FromQuery] PaginationParam<CampaignDetailEnum.CampaignDetailSort> paginationModel,
            [FromQuery] CampaignDetailSearchRequest searchModel
        )
        {
            try
            {
                var start = DateTime.Now;
                var (campaignDetailsList, totalItems) = await _campaignDetailService.GetAsync(paginationModel, searchModel);
                Console.Write(DateTime.Now.Subtract(start).Milliseconds);

                return Ok(new ApiResponse<IList<CampaignPriceResponse>>(
                    campaignDetailsList,
                    "Campaign details fetched successfully.",
                    true,
                    new PaginationInfo(totalItems, paginationModel.PageSize, paginationModel.Page, (int)Math.Ceiling(totalItems / (double)paginationModel.PageSize))
                ));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponse<object>(null, $"Error fetching campaign details: {ex.Message}", false, null));
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCampaignDetailsById(Guid id)
        {
            try
            {
                var campaignDetails = await _campaignDetailService.GetCampaignDetailByIdAsync(id);
                return Ok(new ApiResponse<CampaignPriceResponse>(campaignDetails, "Campaign detail fetched successfully.", true, null));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ApiResponse<string>(null, ex.Message, false, null));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponse<object>(null, $"Error fetching campaign detail: {ex.Message}", false, null));
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCampaignDetails(CampaignPriceCreateRequest model)
        {
            try
            {
                var campaignDetails = await _campaignDetailService.CreateCampaignDetailAsync(model);

                return CreatedAtAction(nameof(GetCampaignDetailsById),
                new { id = campaignDetails.Id },
                                       new ApiResponse<CampaignPriceResponse>(campaignDetails, "Campaign detail created successfully.", true, null));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponse<object>(null, $"Error creating campaign detail: {ex.Message}", false, null));
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCampaignDetails(CampaignPriceUpdateRequest model)
        {
            try
            {
                await _campaignDetailService.UpdateCampaignDetailAsync(model);
                return Ok(new ApiResponse<object>(null, "Campaign detail updated successfully.", true, null));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ApiResponse<object>(null, ex.Message, false, null));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponse<object>(null, $"Error updating campaign detail: {ex.Message}", false, null));
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCampaignDetails(Guid id)
        {
            try
            {
                await _campaignDetailService.DeleteCampaignDetailAsync(id);
                return Ok(new ApiResponse<object>(null, "Campaign detail deleted successfully.", true, null));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ApiResponse<string>(null, ex.Message, false, null));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponse<object>(null, $"Error deleting campaign detail: {ex.Message}", false, null));
            }
        }
    }
}
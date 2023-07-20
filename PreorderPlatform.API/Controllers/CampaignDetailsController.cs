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
using PreorderPlatform.Entity.Models;
using Microsoft.AspNetCore.Authorization;
using PreorderPlatform.Service.Services.CampaignServices;

namespace PreorderPlatform.API.Controllers
{
    [Route("api/campaign/{campaignId}/campaign-details")]
    [ApiController]
    public class CampaignDetailsController : ControllerBase
    {
        private readonly ICampaignDetailService _campaignDetailService;
        private readonly ICampaignService _campaignService;
        private readonly IMapper _mapper;

        public CampaignDetailsController(ICampaignDetailService campaignDetailsService, ICampaignService campaignService, IMapper mapper)
        {
            _campaignService = campaignService;
            _campaignDetailService = campaignDetailsService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCampaignDetails(
            [FromRoute] Guid campaignId,
            [FromQuery] PaginationParam<CampaignDetailEnum.CampaignDetailSort> paginationModel,
            [FromQuery] CampaignDetailSearchRequest searchModel
        )
        {
            try
            {
                var start = DateTime.Now;

                var campaign = await _campaignService.GetCampaignByIdAsync(campaignId); //Get campaign to check if campaign exist

                searchModel.CampaignId = campaignId; //Set campaign id to search model

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
        public async Task<IActionResult> GetCampaignDetailsById(
            [FromRoute] Guid campaignId,
            Guid id
            )
        {
            try
            {
                var campaign = await _campaignService.GetCampaignByIdAsync(campaignId); //Get campaign to check if campaign exist

                var campaignDetails = await _campaignDetailService.GetCampaignDetailByIdAsync(id); //Get campaign detail

                if (campaignDetails.CampaignId != campaignId) //Check if campaign detail is belong to campaign
                { 
                    return StatusCode(StatusCodes.Status404NotFound, new ApiResponse<string>(null, $"Campaign details {campaignDetails.Id} not found in campaign id {campaignId}", false, null));

                }
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
        [Authorize(Policy = "MustBeCampaignOwnerOrStaff")] //Check if user is in a owner or staff of business that have campaign 
        public async Task<IActionResult> CreateCampaignDetails([FromRoute] Guid campaignId, CampaignPriceCreateRequest model)
        {
            try
            {
                model.CampaignId = campaignId; //Set campaign id to model

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
        [Authorize(Policy = "MustBeCampaignOwnerOrStaff")] //Check if user is in a owner or staff of business that have campaign 
        public async Task<IActionResult> UpdateCampaignDetails([FromRoute] Guid campaignId, CampaignPriceUpdateRequest model)
        {
            var campaign = await _campaignService.GetCampaignByIdAsync(campaignId); //Get campaign to check if campaign exist

            if (campaignId != model.CampaignId) //Check if campaign detail is belong to campaign
            {
                return StatusCode(StatusCodes.Status404NotFound, new ApiResponse<string>(null, $"Campaign details {model.Id} not found in campaign id {campaignId}", false, null));
            }

            await _campaignDetailService.UpdateCampaignDetailAsync(model);

            return Ok(new ApiResponse<object>(null, "Campaign detail updated successfully.", true, null));
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "MustBeCampaignOwnerOrStaff")] //Check if user is in a owner or staff of business that have campaign 
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
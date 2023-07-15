using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PreorderPlatform.Service.Enum;
using PreorderPlatform.Service.Exceptions;
using PreorderPlatform.Service.Services.CampaignServices;
using PreorderPlatform.Service.Utility.Pagination;
using PreorderPlatform.Service.ViewModels.ApiResponse;
using PreorderPlatform.Service.ViewModels.Campaign.Request;
using PreorderPlatform.Service.ViewModels.Campaign.Response;

namespace PreorderPlatform.API.Controllers
{
    [Route("api/campaigns")]
    [ApiController]
    public class CampaignsController : ControllerBase
    {
        private readonly ICampaignService _campaignService;
        private readonly IMapper _mapper;

        public CampaignsController(ICampaignService campaignService, IMapper mapper)
        {
            _campaignService = campaignService;
            _mapper = mapper;
        }

        [HttpGet]
        //localhost/filterModelRequest
        public async Task<IActionResult> GetAllCampaigns(
            [FromQuery] PaginationParam<CampaignEnum.CampaignSort> paginationModel,
            [FromQuery] CampaignSearchRequest searchModel
        )
        {
            try
            {
                var start = DateTime.Now;
                var (campaigns, totalItems) = await _campaignService.GetAsync(paginationModel, searchModel);
                Console.Write(DateTime.Now.Subtract(start).Milliseconds);

                return Ok(new ApiResponse<IList<CampaignResponse>>(
                    campaigns,
                    "Campaigns fetched successfully.",
                    true,
                    new PaginationInfo(totalItems, paginationModel.PageSize, paginationModel.Page, (int)Math.Ceiling(totalItems / (double)paginationModel.PageSize))
                ));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponse<object>(null, $"Error fetching campaigns: {ex.Message}", false, null));
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCampaignById(Guid id)
        {
            try
            {
                var campaign = await _campaignService.GetCampaignByIdAsync(id);
                return Ok(new ApiResponse<CampaignDetailResponse>(campaign, "Campaign fetched successfully.", true, null));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ApiResponse<string>(null, ex.Message, false, null));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponse<object>(null, $"Error fetching campaign: {ex.Message}", false, null));
            }
        }

        [HttpPost]
        [Authorize(Policy = "MustBeCampaignOwnerOrStaff")]
        public async Task<IActionResult> CreateCampaign(CampaignCreateRequest model)
        {
            try
            {
                var campaign = await _campaignService.CreateCampaignAsync(model);

                return CreatedAtAction(nameof(GetCampaignById),
                                       new { id = campaign.Id },
                                       new ApiResponse<CampaignResponse>(campaign, "Campaign created successfully.", true, null));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponse<object>(null, $"Error creating campaign: {ex.Message}", false, null));
            }
        }

        [HttpPut]
        [Authorize(Policy = "MustBeCampaignOwnerOrStaff")]
        public async Task<IActionResult> UpdateCampaign(CampaignUpdateRequest model)
        {
            try
            {
                await _campaignService.UpdateCampaignAsync(model);
                return Ok(new ApiResponse<object>(null, "Campaign updated successfully.", true, null));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ApiResponse<object>(null, ex.Message, false, null));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponse<object>(null, $"Error updating campaign: {ex.Message}", false, null));
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "MustBeCampaignOwnerOrStaff")]
        public async Task<IActionResult> DeleteCampaign(Guid id)
        {
            try
            {
                await _campaignService.DeleteCampaignAsync(id);
                return Ok(new ApiResponse<object>(null, "Campaign deleted successfully.", true, null));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new ApiResponse<string>(null, ex.Message, false, null));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponse<object>(null, $"Error deleting campaign: {ex.Message}", false, null));
            }
        }
    }
}
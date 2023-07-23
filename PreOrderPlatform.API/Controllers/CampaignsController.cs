using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PreOrderPlatform.Service.Enums;
using PreOrderPlatform.Service.Services.BusinessServices;
using PreOrderPlatform.Service.Services.CampaignServices;
using PreOrderPlatform.Service.Services.Exceptions;
using PreOrderPlatform.Service.Services.UserServices;
using PreOrderPlatform.Service.Utility.CustomAuthorizeAttribute;
using PreOrderPlatform.Service.Utility.Pagination;
using PreOrderPlatform.Service.ViewModels.ApiResponse;
using PreOrderPlatform.Service.ViewModels.Campaign.Request;
using PreOrderPlatform.Service.ViewModels.Campaign.Response;
using PreOrderPlatform.Service.ViewModels.CampaignPrice.Request;

namespace PreOrderPlatform.API.Controllers
{
    [Route("api/campaigns")]
    [ApiController]
    public class CampaignsController : ControllerBase
    {
        private readonly ICampaignService _campaignService;
        private readonly IUserService _userService;
        private readonly IBusinessService _businessService;
        private readonly IMapper _mapper;

        public CampaignsController(ICampaignService campaignService, IBusinessService businessService, IMapper mapper, IUserService userService)
        {
            _campaignService = campaignService;
            _businessService = businessService;
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
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

        [HttpGet("{campaignId}")]
        public async Task<IActionResult> GetCampaignById(Guid campaignId)
        {
            try
            {
                var campaign = await _campaignService.GetCampaignByIdAsync(campaignId);
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
        [CustomAuthorize(Roles = "ADMIN,BUSINESS_OWNER,BUSINESS_STAFF")]
        public async Task<IActionResult> CreateCampaign(CampaignCreateRequest model)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var user = await _userService.GetUserByIdAsync(Guid.Parse(userId));

                model.OwnerId = user.Id;
                model.BusinessId = (Guid) user.BusinessId;

                var campaign = await _campaignService.CreateCampaignAsync(model);

                return CreatedAtAction(nameof(GetCampaignById),
                                       new { campaignId = campaign.Id },
                                       new ApiResponse<CampaignDetailResponse>(campaign, "Campaign created successfully.", true, null));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiResponse<object>(null, $"Error creating campaign: {ex.Message}", false, null));
            }
        }


        // Controller
        [HttpPut("{campaignId}/status")]
        [Authorize(Policy = "MustBeCampaignOwnerOrStaff")] //Check if user is owner of the campaign or staff of business have access to the campaign
        public async Task<IActionResult> ChangeCampaignStatusAsync(Guid campaignId, [FromBody] ChangeCampaignStatusRequest model)
        {          
            await _campaignService.ChangeCampaignStatusAsync(campaignId, model.Status);
            return Ok(new ApiResponse<object>(null, "Campaign status updated successfully.", true, null));          
        }

        // PUT api/campaigns/{campaignId}/details
        [HttpPut("{campaignId}/details")]
        [Authorize(Policy = "MustBeCampaignOwnerOrStaff")] // Check if user is owner of the campaign or staff of business have access to the campaign
        public async Task<IActionResult> UpdateCampaignDetails(Guid campaignId, [FromBody] List<CampaignPriceUpdateRequest> model)
        {         
            await _campaignService.UpdateCampaignDetailsAsync(campaignId, model);
            return Ok(new ApiResponse<object>(null, "Campaign details updated successfully.", true, null)); 
        }

        [HttpPut("{campaignId}")]
        [Authorize(Policy = "MustBeCampaignOwnerOrStaff")] //Check if user is owner of the campaign or staff of business have access to the campaign
        public async Task<IActionResult> UpdateCampaign(Guid campaignId, [FromBody] CampaignUpdateRequest model)
        {
            try
            {
                model.Id = campaignId;
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

        // DELETE api/campaigns/{campaignId}/details/{phase}
        [HttpDelete("{campaignId}/details/{phase}")]
        [Authorize(Policy = "MustBeCampaignOwnerOrStaff")]
        public async Task<IActionResult> DeleteCampaignDetail(Guid campaignId, int phase)
        {
            Console.WriteLine($"Delete campaign detail campaignId {campaignId} phase {phase}");
            await _campaignService.DeleteCampaignDetailAsync(campaignId, phase);
            return Ok(new ApiResponse<object>(null, "Campaign detail deleted successfully.", true, null));
        }


        [HttpDelete("{campaignId}")]
        [Authorize(Policy = "MustBeCampaignOwnerOrStaff")] //Check if user is owner of the campaign or staff of business have access to the campaign
        public async Task<IActionResult> DeleteCampaign(Guid campaignId)
        {
            try
            {
                //Campaign must be in draft status to be deleted
                await _campaignService.DeleteCampaignAsync(campaignId);
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
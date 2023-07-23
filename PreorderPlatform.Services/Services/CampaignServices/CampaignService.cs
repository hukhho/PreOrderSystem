using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PreOrderPlatform.Entity.Enum.Campaign;
using PreOrderPlatform.Entity.Models;
using PreOrderPlatform.Entity.Repositories.CampaignRepositories;
using PreOrderPlatform.Entity.Repositories.ProductRepositories;
using PreOrderPlatform.Service.Enums;
using PreOrderPlatform.Service.Helpers;
using PreOrderPlatform.Service.Services.Exceptions;
using PreOrderPlatform.Service.Utility;
using PreOrderPlatform.Service.Utility.Pagination;
using PreOrderPlatform.Service.ViewModels.Campaign.Request;
using PreOrderPlatform.Service.ViewModels.Campaign.Response;
using PreOrderPlatform.Service.ViewModels.CampaignPrice.Request;

namespace PreOrderPlatform.Service.Services.CampaignServices
{
    public class CampaignService : ICampaignService
    {
        private readonly ICampaignRepository _campaignRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public CampaignService(ICampaignRepository campaignRepository, IMapper mapper, IProductRepository productRepository)
        {
            _productRepository = productRepository;
            _campaignRepository = campaignRepository;
            _mapper = mapper;
        }

        public async Task<List<CampaignResponse>> GetCampaignsAsync()
        {
            try
            {
                var campaigns = await _campaignRepository.GetAllAsync();
                return _mapper.Map<List<CampaignResponse>>(campaigns);
            }
            catch (Exception ex)
            {
                throw new ServiceException("An error occurred while fetching campaigns.", ex);
            }
        }

        //GetAllCampaignsWithOwnerAndBusinessAndCampaignDetailsAsync
        public async Task<List<CampaignRepository>> GetAllCampaignsWithOwnerAndBusinessAndCampaignDetailsAsync()
        {
            try
            {
                var campaigns = await _campaignRepository.GetAllCampaignsWithOwnerAndBusinessAndCampaignDetailsAsync();
                return _mapper.Map<List<CampaignRepository>>(campaigns);
            }
            catch (Exception ex)
            {
                throw new ServiceException("An error occurred while fetching campaigns.", ex);
            }
        }

    
        public async Task<CampaignDetailResponse> GetCampaignByIdAsync(Guid id)
        {
            try
            {
                // Fetch the campaign with all related details
                var campaign = await _campaignRepository.GetCampaignWithDetailsAsync(id);

                // If no campaign was found, throw a NotFoundException
                if (campaign == null)
                {
                    throw new NotFoundException($"Campaign with ID {id} was not found.");
                }

                // Map the campaign entity to a response model and return it
                return _mapper.Map<CampaignDetailResponse>(campaign);
            }
            catch (NotFoundException)
            {
                // Rethrow NotFoundException to be handled by the caller
                throw;
            }
            catch (Exception ex)
            {
                // If any other error occurred, wrap it in a ServiceException and throw it
                throw new ServiceException($"An error occurred while fetching campaign with ID {id}.", ex);
            }
        }
        public async Task<CampaignDetailResponse> CreateCampaignAsync(CampaignCreateRequest model)
        {
            try
            {
                if (model.CampaignDetails == null || model.CampaignDetails.Count == 0)
                {
                    throw new ServiceException("Campaign price is required.");
                }
                var product = await _productRepository.GetByIdAsync(model.ProductId);
                if (product == null)
                {
                    throw new ServiceException($"Product with ID {model.ProductId} was not found.");
                } 
                if (product.BusinessId != model.BusinessId)
                {
                    throw new ServiceException($"Product with ID {model.ProductId} does not belong to business with ID {model.BusinessId}.");
                }
                // Sort the campaignDetails list by `phase`
                var sortedCampaignDetails = model.CampaignDetails.OrderBy(d => d.Phase).ToList();

                // Check if each `phase` is exactly one more than the previous `phase`
                for (int i = 0; i < sortedCampaignDetails.Count; i++)
                {
                    if (sortedCampaignDetails[i].Phase != i + 1)
                    {
                        throw new ServiceException($"Campaign phases must be sequential starting from 1. Found unexpected phase {sortedCampaignDetails[i].Phase} at position {i + 1}.");
                    }
                }

                if (model.EndAt <= model.StartAt) {    
                    Console.WriteLine($"End date must be greater than start date. EndAt {model.EndAt} StartAt {model.StartAt}");
                    throw new ServiceException("End date must be greater than start date.");
                } 
                if (model.StartAt < DateTimeUtcPlus7.Now)
                {
                    throw new ServiceException("Start date must be greater than current date.");
                } 
                if (model.ExpectedShippingDate <= model.EndAt)
                {
                    throw new ServiceException("Expected shipping date must be greater than end date.");
                }
                if (model.ExpectedShippingDate <= DateTimeUtcPlus7.Now)
                {
                    throw new ServiceException("Expected shipping date must be greater than current date.");
                }
                
                var campaign = _mapper.Map<Campaign>(model);

                campaign.CreatedAt = DateTimeUtcPlus7.Now;
                campaign.UpdatedAt = DateTimeUtcPlus7.Now;

                campaign.Status = CampaignStatus.Draft;

                await _campaignRepository.CreateAsync(campaign);

                return _mapper.Map<CampaignDetailResponse>(campaign);
            }
            catch (ServiceException ex)
            {
                throw ex;
            }
            catch (NotFoundException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new ServiceException("An error occurred while creating the campaign.", ex);
            }
        }

        public async Task UpdateCampaignDetailsAsync(Guid campaignId, List<CampaignPriceUpdateRequest> newDetails)
        {
            var existingCampaign = await _campaignRepository.GetCampaignWithDetailsAsync(campaignId);
            
            if (existingCampaign == null)
            {
                throw new NotFoundException($"Campaign with id {campaignId} does not exist.");
            }
            if (existingCampaign.Status != CampaignStatus.Draft && existingCampaign.Status != CampaignStatus.Paused)
            {
                throw new ServiceException($"Campaign with id {campaignId} is not in draft status or paused status.");
            }
            foreach (var detail in newDetails)
            {
                var existingDetail = existingCampaign.CampaignDetails.FirstOrDefault(x => x.Id == detail.Id);

                if (existingDetail == null)
                {
                    throw new NotFoundException($"Campaign detail with id {detail.Id} does not exist.");
                }
                if (existingDetail.TotalOrdered > 0)
                {
                    throw new ServiceException($"Campaign detail with id {detail.Id} has already been ordered.");
                }

                existingDetail.AllowedQuantity = detail.AllowedQuantity;
                existingDetail.Price = detail.Price;
            }

            await _campaignRepository.UpdateAsync(existingCampaign);
        }

        public async Task DeleteCampaignDetailAsync(Guid campaignId, int phase)
        {
            var campaign = await _campaignRepository.GetCampaignWithDetailsAsync(campaignId);

            if (campaign == null)
            {
                throw new NotFoundException($"Campaign with id {campaignId} does not exist.");
            }
            if (campaign.Status != CampaignStatus.Draft && campaign.Status != CampaignStatus.Paused)
            {
                throw new ServiceException($"Campaign with id {campaignId} is not in draft status.");
            }   
            var detail = campaign.CampaignDetails.FirstOrDefault(d => d.Phase == phase);

            if (detail == null)
            {
                throw new NotFoundException($"Campaign detail with phase {phase} does not exist.");
            }
            if (campaign.Status == CampaignStatus.Paused && detail.TotalOrdered > 0)
            {
                  throw new ServiceException($"Campaign detail with phase {phase} has already been ordered.");
            }

            campaign.CampaignDetails.Remove(detail);

            // Update the phase numbers for the remaining details
            foreach (var remainingDetail in campaign.CampaignDetails.Where(d => d.Phase > phase))
            {
                remainingDetail.Phase--;
            }

            await _campaignRepository.UpdateAsync(campaign);
        }

        public async Task ChangeCampaignStatusAsync(Guid campaignId, CampaignStatus newStatus)
        {
            var campaign = await _campaignRepository.GetByIdAsync(campaignId);

            if (campaign == null)
            {
                throw new ServiceException("Campaign not found.");
            }

            switch (campaign.Status)
            {
                case CampaignStatus.Draft:
                    // From Draft, you can only move to Scheduled or Cancelled.
                    if (newStatus != CampaignStatus.Scheduled && newStatus != CampaignStatus.Cancelled)
                    {
                        throw new ServiceException("Invalid status transition. A campaign can only be changed from Draft to Scheduled or Cancelled.");
                    }
                    break;
                case CampaignStatus.Scheduled:
                    // From Scheduled, you can move to Running, Paused, or Cancelled.
                    if (newStatus != CampaignStatus.Running && newStatus != CampaignStatus.Paused && newStatus != CampaignStatus.Cancelled)
                    {
                        throw new ServiceException("Invalid status transition. A campaign can only be changed from Scheduled to Running, Paused, or Cancelled.");
                    }
                    break;
                case CampaignStatus.Running:
                    // From Running, you can move to Paused, Completed, or Cancelled.
                    if (newStatus != CampaignStatus.Paused && newStatus != CampaignStatus.Completed && newStatus != CampaignStatus.Cancelled)
                    {
                        throw new ServiceException("Invalid status transition. A campaign can only be changed from Running to Paused, Completed, or Cancelled.");
                    }
                    break;
                case CampaignStatus.Paused:
                    // From Paused, you can move to Running, Completed, or Cancelled.
                    if (newStatus != CampaignStatus.Running && newStatus != CampaignStatus.Completed && newStatus != CampaignStatus.Cancelled)
                    {
                        throw new ServiceException("Invalid status transition. A campaign can only be changed from Paused to Running, Completed, or Cancelled.");
                    }
                    break;
                case CampaignStatus.Completed:
                    // From Completed, you cannot move to any other status.
                    if (newStatus != CampaignStatus.Completed)
                    {
                        throw new ServiceException("Invalid status transition. A completed campaign cannot change its status.");
                    }
                    break;
                case CampaignStatus.Cancelled:
                    // From Cancelled, you cannot move to any other status.
                    if (newStatus != CampaignStatus.Cancelled)
                    {
                        throw new ServiceException("Invalid status transition. A cancelled campaign cannot change its status.");
                    }
                    break;
                default:
                    throw new ServiceException("Invalid current status.");
            }

            campaign.Status = newStatus;
            await _campaignRepository.UpdateAsync(campaign);
        }

        public async Task UpdateCampaignAsync(CampaignUpdateRequest model)
        {
            try
            {
                var campaign = await _campaignRepository.GetByIdAsync(model.Id);
                if (campaign.Status != CampaignStatus.Draft)
                {
                    throw new ServiceException("Campaign is not in draft status.");
                }
                campaign = _mapper.Map(model, campaign);
                await _campaignRepository.UpdateAsync(campaign);
            }
            catch (Exception ex)
            {
                throw new ServiceException($"An error occurred while updating campaign with ID {model.Id}.", ex);
            }
        }

        public async Task DeleteCampaignAsync(Guid id)
        {
            try
            {              
                var campaign = await _campaignRepository.GetByIdAsync(id);

                if (campaign == null)
                {
                    throw new NotFoundException($"Campaign with id {id} does not exist.");
                } 
                if (campaign.Status != CampaignStatus.Draft)
                {
                    throw new ServiceException($"Campaign with id {id} is not in draft status.");
                }
                await _campaignRepository.DeleteAsync(campaign);
            }
            catch (Exception ex)
            {
                throw new ServiceException($"An error occurred while deleting campaign with ID {id}.", ex);
            }
        }    

        public async Task<(IList<CampaignResponse> campaigns, int totalItems)> GetAsync(PaginationParam<CampaignEnum.CampaignSort> paginationModel, CampaignSearchRequest filterModel)
        {
            try
            {
                var dateInRange = filterModel.DateInRange;
                filterModel.DateInRange = null;

                var query = _campaignRepository.Table;

                query = query.GetWithSearch(filterModel); //search
                query = query.FilterByDateInRange(dateInRange, e => e.StartAt, e => e.EndAt); // Filter by date range

                // Calculate the total number of items before applying pagination
                int totalItems = await query.CountAsync();

                query = query.GetWithSorting(paginationModel.SortKey.ToString(), paginationModel.SortOrder) //sort
                            .GetWithPaging(paginationModel.Page, paginationModel.PageSize);  // pagination

                var campaignList = await query.ToListAsync(); // Call ToListAsync here

                // Map the campaignList to a list of CampaignResponse objects
                var result = _mapper.Map<List<CampaignResponse>>(campaignList);

                return (result, totalItems);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception " + ex.Message);
                throw new ServiceException("An error occurred while fetching campaigns.", ex);
            }
        }

    }
}


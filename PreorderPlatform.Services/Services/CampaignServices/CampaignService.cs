using AutoMapper;
using PreorderPlatform.Entity.Models;
using PreorderPlatform.Entity.Repositories.CampaignRepositories;
using PreorderPlatform.Service.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PreorderPlatform.Service.Utility.Pagination;
using PreorderPlatform.Service.ViewModels.Campaign.Response;
using PreorderPlatform.Service.ViewModels.Campaign.Request;
using PreorderPlatform.Service.Enum;
using Microsoft.EntityFrameworkCore;
using PreorderPlatform.Service.Utility;

namespace PreorderPlatform.Service.Services.CampaignServices
{
    public class CampaignService : ICampaignService
    {
        private readonly ICampaignRepository _campaignRepository;
        private readonly IMapper _mapper;

        public CampaignService(ICampaignRepository campaignRepository, IMapper mapper)
        {
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
var campaign = await _campaignRepository.GetCampaignWithDetailsAsync(id);

                if (campaign == null)
                {
throw new NotFoundException($"Campaign with ID {id} was not found.");
                }

                return _mapper.Map<CampaignDetailResponse>(campaign);
            }
            catch (NotFoundException)
            {
                // Rethrow NotFoundException to be handled by the caller
                throw;
            }
            catch (Exception ex)
            {
throw new ServiceException($"An error occurred while fetching campaign with ID {id}.", ex);
            }
        }

        public async Task<CampaignResponse> CreateCampaignAsync(CampaignCreateRequest model)
        {
            try
            {
                var campaign = _mapper.Map<Campaign>(model);
                await _campaignRepository.CreateAsync(campaign);
                return _mapper.Map<CampaignResponse>(campaign);
            }
            catch (Exception ex)
            {
                throw new ServiceException("An error occurred while creating the campaign.", ex);
            }
        }

        public async Task UpdateCampaignAsync(CampaignUpdateRequest model)
        {
            try
            {
var campaign = await _campaignRepository.GetByIdAsync(model.Id);
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


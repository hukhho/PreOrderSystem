﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PreOrderPlatform.Entity.Models;
using PreOrderPlatform.Entity.Repositories.CampaignDetailRepositories;
using PreOrderPlatform.Service.Enums;
using PreOrderPlatform.Service.Services.Exceptions;
using PreOrderPlatform.Service.Utility;
using PreOrderPlatform.Service.Utility.Pagination;
using PreOrderPlatform.Service.ViewModels.CampaignPrice.Request;
using PreOrderPlatform.Service.ViewModels.CampaignPrice.Response;

namespace PreOrderPlatform.Service.Services.CampaignDetailServices
{
    public class CampaignDetailService : ICampaignDetailService
    {
        private readonly ICampaignDetailRepository _campaignDetailRepository;
        private readonly IMapper _mapper;

        public CampaignDetailService(ICampaignDetailRepository campaignDetailRepository, IMapper mapper)
        {
            _campaignDetailRepository = campaignDetailRepository;
            _mapper = mapper;
        }

        public async Task<List<CampaignPriceResponse>> GetCampaignDetailsAsync()
        {
            try
            {
                var campaignDetails = await _campaignDetailRepository.GetAllAsync();
                return _mapper.Map<List<CampaignPriceResponse>>(campaignDetails);
            }
            catch (Exception ex)
            {
                throw new ServiceException("An error occurred while fetching campaign details.", ex);
            }
        }

        //GetAllCampainDetailsWithProductAsync
        public async Task<List<CampaignPriceResponse>> GetAllCampainDetailsWithProductAsync()
        {
            try
            {
                var campaignDetails = await _campaignDetailRepository.GetAllCampainDetailsAsync();
                return _mapper.Map<List<CampaignPriceResponse>>(campaignDetails);
            }
            catch (Exception ex)
            {
                throw new ServiceException("An error occurred while fetching campaign details.", ex);
            }
        }

public async Task<CampaignPriceResponse> GetCampaignDetailByIdAsync(Guid id)
        {
            try
            {
var campaignDetail = await _campaignDetailRepository.GetByIdAsync(id);

                if (campaignDetail == null)
                {
                    throw new NotFoundException($"Campaign detail with ID {id} was not found.");
                }

                return _mapper.Map<CampaignPriceResponse>(campaignDetail);
            }
            catch (NotFoundException)
            {
                // Rethrow NotFoundException to be handled by the caller
                throw;
            }
            catch (Exception ex)
            {
throw new ServiceException($"An error occurred while fetching campaign detail with ID {id}.", ex);
            }
        }

        public async Task<CampaignPriceResponse> CreateCampaignDetailAsync(CampaignPriceCreateRequest model)
        {
            try
            {
                var campaignDetail = _mapper.Map<CampaignDetail>(model);

                // Set the phase to be 1 more than the highest existing phase
                campaignDetail.Phase = (await _campaignDetailRepository.GetMaxPhaseAsync()) + 1;

                await _campaignDetailRepository.CreateAsync(campaignDetail);
                return _mapper.Map<CampaignPriceResponse>(campaignDetail);
            }
            catch (Exception ex)
            {
                throw new ServiceException("An error occurred while creating the campaign detail.", ex);
            }
        }


        public async Task UpdateCampaignDetailAsync(CampaignPriceUpdateRequest model)
        {
            try
            {
                var campaignDetail = await _campaignDetailRepository.GetByIdAsync(model.Id);
                campaignDetail = _mapper.Map(model, campaignDetail);
                await _campaignDetailRepository.UpdateAsync(campaignDetail);
            }
            catch (Exception ex)
            {
                throw new ServiceException($"An error occurred while updating campaign detail with ID {model.Id}.", ex);
            }
        }

        public async Task DeleteCampaignDetailAsync(Guid id)
        {
            try
            {
                var campaignDetail = await _campaignDetailRepository.GetByIdAsync(id);

                // Only allow deletion of the highest phase
                if (campaignDetail.Phase == await _campaignDetailRepository.GetMaxPhaseAsync())
                {
                    await _campaignDetailRepository.DeleteAsync(campaignDetail);
                }
                else
                {
                    throw new InvalidOperationException("Only the highest phase can be removed.");
                }
            }
            catch (Exception ex)
            {
                throw new ServiceException($"An error occurred while deleting campaign detail with ID {id}.", ex);
            }
        }


        public async Task<(IList<CampaignPriceResponse> campaigns, int totalItems)> GetAsync(PaginationParam<CampaignDetailEnum.CampaignDetailSort> paginationModel, CampaignDetailSearchRequest filterModel)
        {
            try
            {

                var query = _campaignDetailRepository.Table;

                query = query.GetWithSearch(filterModel); //search

                // Calculate the total number of items before applying pagination
                int totalItems = await query.CountAsync();

                query = query.GetWithSorting(paginationModel.SortKey.ToString(), paginationModel.SortOrder) //sort
                            .GetWithPaging(paginationModel.Page, paginationModel.PageSize);  // pagination

                var campaignList = await query.ToListAsync(); // Call ToListAsync here

                // Map the campaignList to a list of CampaignPriceResponse objects
                var result = _mapper.Map<List<CampaignPriceResponse>>(campaignList);

                return (result, totalItems);
            }

            catch (Exception ex)
            {
                Console.WriteLine("Exception " + ex.Message);
                throw new ServiceException("An error occurred while fetching campaign details.", ex);
            }
        }
        

    }



}


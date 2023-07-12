using AutoMapper;
using PreorderPlatform.Entity.Models;
using PreorderPlatform.Entity.Repositories.BusinessRepositories;
using PreorderPlatform.Service.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PreorderPlatform.Service.ViewModels.Business.Request;
using PreorderPlatform.Service.ViewModels.Business.Response;
using PreorderPlatform.Service.Utility.Pagination;
using PreorderPlatform.Services.Enum;
using PreorderPlatform.Services.ViewModels.Business.Request;
using PreorderPlatform.Service.Utility;
using Microsoft.EntityFrameworkCore;

namespace PreorderPlatform.Service.Services.BusinessServices
{
    public class BusinessService : IBusinessService
    {
        private readonly IBusinessRepository _businessRepository;
        private readonly IMapper _mapper;

        public BusinessService(IBusinessRepository businessRepository, IMapper mapper)
        {
            _businessRepository = businessRepository;
            _mapper = mapper;
        }

        public async Task<List<BusinessResponse>> GetBusinessesAsync()
        {
            try
            {
                var businesses = await _businessRepository.GetAllAsync();
                return _mapper.Map<List<BusinessResponse>>(businesses);
            }
            catch (Exception ex)
            {
                throw new ServiceException("An error occurred while fetching businesses.", ex);
            }
        }

        public async Task<BusinessByIdResponse> GetBusinessByIdAsync(int id, string? userId)
        {
            try
            {
                var business = await _businessRepository.GetBusinessByIdAsync(id);

                if (business == null)
                {
                    throw new NotFoundException($"Business with ID {id} was not found.");
                }
                if (business.Owner.Id != int.Parse(userId))
                {
                    throw new ArgumentException($"You don't have permission to access this resource.");
                }
                return _mapper.Map<BusinessByIdResponse>(business);

            }
            catch (NotFoundException)
            {
                // Rethrow NotFoundException to be handled by the caller
                throw;
            }
            catch (ArgumentException ex)
            {
                // Rethrow ArgumentException to be handled by the caller
                throw;
            }
            catch (Exception ex)
            {
                throw new ServiceException($"An error occurred while fetching business with ID {id}.", ex);
            }
        }

        public async Task<BusinessResponse> CreateBusinessAsync(BusinessCreateRequest model)
        {
            try
            {
                var business = _mapper.Map<Business>(model);
                await _businessRepository.CreateAsync(business);
                return _mapper.Map<BusinessResponse>(business);
            }
            catch (Exception ex)
            {
                throw new ServiceException("An error occurred while creating the business.", ex);
            }
        }

        public async Task UpdateBusinessAsync(BusinessUpdateRequest model)
        {
            try
            {
                var business = await _businessRepository.GetByIdAsync(model.Id);
                business = _mapper.Map(model, business);
                await _businessRepository.UpdateAsync(business);
            }
            catch (Exception ex)
            {
                throw new ServiceException($"An error occurred while updating business with ID {model.Id}.", ex);
            }
        }

        public async Task DeleteBusinessAsync(int id)
        {
            try
            {
                var business = await _businessRepository.GetByIdAsync(id);
                await _businessRepository.DeleteAsync(business);
            }
            catch (Exception ex)
            {
                throw new ServiceException($"An error occurred while deleting business with ID {id}.", ex);
            }
        }


        public async Task<(IList<BusinessResponse> businesses, int totalItems)> GetAsync(PaginationParam<BusinessEnum.BusinessSort> paginationModel, BusinessSearchRequest filterModel)
        {
            try
            {
                var query = _businessRepository.Table;

                query = query.GetWithSearch(filterModel); //search
                

                // Calculate the total number of items before applying pagination
                int totalItems = await query.CountAsync();

                query = query.GetWithSorting(paginationModel.SortKey.ToString(), paginationModel.SortOrder) //sort
                            .GetWithPaging(paginationModel.Page, paginationModel.PageSize);  // pagination

                var businessList = await query.ToListAsync(); // Call ToListAsync here

                // Map the businessList to a list of BusinessResponse objects
                var result = _mapper.Map<List<BusinessResponse>>(businessList);

                return (result, totalItems);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception " + ex.Message);
                throw new ServiceException("An error occurred while fetching businesses.", ex);
            }
        }
        

    }
}
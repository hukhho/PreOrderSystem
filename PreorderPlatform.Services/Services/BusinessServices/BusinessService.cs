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
using PreorderPlatform.Entity.Repositories.UserRepositories;

namespace PreorderPlatform.Service.Services.BusinessServices
{
    public class BusinessService : IBusinessService
    {
        private readonly IBusinessRepository _businessRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public BusinessService(IBusinessRepository businessRepository, IUserRepository userRepository, IMapper mapper)
        {
            _businessRepository = businessRepository;
            _userRepository = userRepository;
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

public async Task<BusinessByIdResponse> GetBusinessByIdAsync(Guid id)
        {
            try
            {
                var business = await _businessRepository.GetBusinessByIdAsync(id);

                if (business == null)
                {
                    throw new NotFoundException($"Business with ID {id} was not found.");
                }
                return _mapper.Map<BusinessByIdResponse>(business);

            }
            catch (NotFoundException)
            {
                // Rethrow NotFoundException to be handled by the caller
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error {ex}");

                throw new ServiceException($"An error occurred while fetching business with ID {id}.", ex);
            }
        }

        public async Task<BusinessResponse> CreateBusinessAsync(BusinessCreateRequest model)
        {
            try
            {

                // Retrieve the user by userId
                var user = await _userRepository.GetByIdAsync(model.OwnerId);

                if (user == null)
                {
                    throw new ServiceException("User not found.");
                }

                // Check if the user already has a business associated
                if (user.BusinessId.HasValue)
                {
                    throw new ServiceException("User can only create one business.");
                }

                var business = _mapper.Map<Business>(model);
                await _businessRepository.CreateAsync(business);

                // Update user's BusinessId
                user.BusinessId = business.Id;

                // Save the updated user
                await _userRepository.UpdateAsync(user);

                return _mapper.Map<BusinessResponse>(business);
            }
            catch (ServiceException)
            {
                // Rethrow ServiceException to be handled by the caller
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error {ex}");
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
                Console.WriteLine($"Error {ex}");
                throw new ServiceException($"An error occurred while updating business with ID {model.Id}.", ex);
            }
        }

//public async Task DeleteBusinessAsync(Guid id)
//        {
//            var user = await _userRepository.GetByIdAsync(model.OwnerGuid);
//            user.BusinessId = business.Guid;
//            var business = await _businessRepository.GetByIdAsync(model.Guid);
//            throw new ServiceException($"An error occurred while updating business with ID {model.Guid}.", ex);
//            {
//                try
//                {
//                    var business = await _businessRepository.GetByIdAsync(id);
//                    await _businessRepository.DeleteAsync(business);
//                }
//                catch (Exception ex)
//                {
//                    throw new ServiceException($"An error occurred while deleting business with ID {id}.", ex);
//                }
//            }
//        }



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


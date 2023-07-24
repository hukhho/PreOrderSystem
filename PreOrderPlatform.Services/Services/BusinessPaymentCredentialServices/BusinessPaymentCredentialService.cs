using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PreOrderPlatform.Entity.Models;
using PreOrderPlatform.Entity.Repositories.BusinessPaymentCredentialRepositories;
using PreOrderPlatform.Entity.Repositories.BusinessRepositories;
using PreOrderPlatform.Service.Enums;
using PreOrderPlatform.Service.Services.Exceptions;
using PreOrderPlatform.Service.Utility;
using PreOrderPlatform.Service.Utility.Pagination;
using PreOrderPlatform.Service.ViewModels.BusinessPaymentCredential;

namespace PreOrderPlatform.Service.Services.BusinessPaymentCredentialServices
{
    public class BusinessPaymentCredentialService : IBusinessPaymentCredentialService
    {
        private readonly IBusinessPaymentCredentialRepository _businessPaymentCredentialRepository;

        private readonly IBusinessRepository _businessRepository;

        private readonly IMapper _mapper;

        public BusinessPaymentCredentialService(
            IBusinessPaymentCredentialRepository businessPaymentCredentialRepository,
            IBusinessRepository businessRepository,
            IMapper mapper
        )
        {
            _businessPaymentCredentialRepository = businessPaymentCredentialRepository;
            _businessRepository = businessRepository;
            _mapper = mapper;
        }

        public async Task<
            List<BusinessPaymentCredentialViewModel>
        > GetBusinessPaymentCredentialsAsync()
        {
            try
            {
                var businessPaymentCredentials =
                    await _businessPaymentCredentialRepository.GetAllAsync();
                return _mapper.Map<List<BusinessPaymentCredentialViewModel>>(
                    businessPaymentCredentials
                );
            }
            catch (Exception ex)
            {
                throw new ServiceException(
                    "An error occurred while fetching business payment credentials.",
                    ex
                );
            }
        }

        public async Task<BusinessPaymentCredentialViewModel> GetBusinessPaymentCredentialByIdAsync(
            Guid id
        )
        {
            try
            {
                var businessPaymentCredential =
                    await _businessPaymentCredentialRepository.GetBusinessPaymentCredentialByIdAsync(
                        id
                    );

                if (businessPaymentCredential == null)
                {
                    throw new NotFoundException(
                        $"Business payment credential with ID {id} was not found."
                    );
                }

                return _mapper.Map<BusinessPaymentCredentialViewModel>(businessPaymentCredential);
            }
            catch (NotFoundException)
            {
                // Rethrow NotFoundException to be handled by the caller
                throw;
            }
            catch (Exception ex)
            {
                throw new ServiceException(
                    $"An error occurred while fetching business payment credential with ID {id}.",
                    ex
                );
            }
        }

        public async Task<BusinessPaymentCredentialViewModel> CreateBusinessPaymentCredentialAsync(
            BusinessPaymentCredentialCreateViewModel model
        )
        {
            try
            {
                var businessPaymentCredential = _mapper.Map<BusinessPaymentCredential>(model);
                await _businessPaymentCredentialRepository.CreateAsync(businessPaymentCredential);
                return _mapper.Map<BusinessPaymentCredentialViewModel>(businessPaymentCredential);
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
                throw new ServiceException(
                    "An error occurred while creating the business payment credential.",
                    ex
                );
            }
        }

        public async Task UpdateBusinessPaymentCredentialAsync(
            Guid id,
            BusinessPaymentCredentialUpdateViewModel model
        )
        {
            try
            {
                var businessPaymentCredential =
                    await _businessPaymentCredentialRepository.GetByIdAsync(id);
                businessPaymentCredential = _mapper.Map(model, businessPaymentCredential);
                await _businessPaymentCredentialRepository.UpdateAsync(businessPaymentCredential);
            }
            catch (Exception ex)
            {
                throw new ServiceException(
                    $"An error occurred while updating business payment credential with ID {id}.",
                    ex
                );
            }
        }

        public async Task<bool> SetMainBusinessPaymentCredentialAsync(
            Guid id
        )
        {
            try
            {
                var businessPaymentCredential =
                    await _businessPaymentCredentialRepository.GetByIdAsync(id);

                var businessId = businessPaymentCredential.BusinessId;

                var businessPaymentCredentials =
                    await _businessPaymentCredentialRepository.GetAllAsync();
                foreach (var paymentCredential in businessPaymentCredentials)
                {
                    paymentCredential.IsMain = false;
                    if (paymentCredential.Id == id)
                    {
                        paymentCredential.IsMain = true;
                    }
                }

                await _businessPaymentCredentialRepository.UpdateMultiAsync(businessPaymentCredentials);
                return true;
            }
            catch (Exception ex)
            {
                throw new ServiceException(
                    $"An error occurred while updating business payment credential with ID {id}.",
                    ex
                );
            }
        }

        public async Task DeleteBusinessPaymentCredentialAsync(Guid id)
        {
            try
            {
                var businessPaymentCredential =
                    await _businessPaymentCredentialRepository.GetByIdAsync(id);
                await _businessPaymentCredentialRepository.DeleteAsync(businessPaymentCredential);
            }
            catch (Exception ex)
            {
                throw new ServiceException(
                    $"An error occurred while deleting business payment credential with ID {id}.",
                    ex
                );
            }
        }

        public async Task<(
            IList<BusinessPaymentCredentialViewModel> businessPaymentCredentials,
            int totalItems
        )> GetAsync(
            PaginationParam<BusinessPaymentCredentialEnum.BusinessPaymentCredentialSort> paginationModel,
            BusinessPaymentCredentialSearchRequest filterModel
        )
        {
            try
            {
                var query = _businessPaymentCredentialRepository.Table;

                query = query.GetWithSearch(filterModel); //search

                // Calculate the total number of items before applying pagination
                int totalItems = await query.CountAsync();

                query = query
                    .GetWithSorting(paginationModel.SortKey.ToString(), paginationModel.SortOrder) //sort
                    .GetWithPaging(paginationModel.Page, paginationModel.PageSize); // pagination

                var businessPaymentCredentialList = await query.ToListAsync(); // Call ToListAsync here

                // Map the businessPaymentCredentialList to a list of BusinessPaymentCredentialViewModel objects
                var result = _mapper.Map<List<BusinessPaymentCredentialViewModel>>(
                    businessPaymentCredentialList
                );

                return (result, totalItems);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception " + ex.Message);
                throw new ServiceException(
                    "An error occurred while fetching business payment credentials.",
                    ex
                );
            }
        }

        public async Task<Business> GetBusinessByOwnerIdAsync(Guid userId)
        {
            try
            {
                var business = await _businessRepository.GetByOwnerIdAsync(userId);

                if (business == null)
                {
                    throw new NotFoundException($"No business found for user with ID {userId}.");
                }

                return business;
            }
            catch (NotFoundException)
            {
                // Rethrow NotFoundException to be handled by the caller
                throw;
            }
            catch (ArgumentException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ServiceException(
                    $"An error occurred while fetching the business for user with ID {userId}.",
                    ex
                );
            }
        }
    }
}

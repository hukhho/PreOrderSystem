using AutoMapper;
using PreorderPlatform.Entity.Models;
using PreorderPlatform.Entity.Repositories.BusinessPaymentCredentialRepositories;
using PreorderPlatform.Service.ViewModels.BusinessPaymentCredential;
using PreorderPlatform.Service.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreorderPlatform.Service.Services.BusinessPaymentCredentialServices
{
    public class BusinessPaymentCredentialService : IBusinessPaymentCredentialService
    {
        private readonly IBusinessPaymentCredentialRepository _businessPaymentCredentialRepository;
        private readonly IMapper _mapper;

        public BusinessPaymentCredentialService(IBusinessPaymentCredentialRepository businessPaymentCredentialRepository, IMapper mapper)
        {
            _businessPaymentCredentialRepository = businessPaymentCredentialRepository;
            _mapper = mapper;
        }

        public async Task<List<BusinessPaymentCredentialViewModel>> GetBusinessPaymentCredentialsAsync()
        {
            try
            {
                var businessPaymentCredentials = await _businessPaymentCredentialRepository.GetAllAsync();
                return _mapper.Map<List<BusinessPaymentCredentialViewModel>>(businessPaymentCredentials);
            }
            catch (Exception ex)
            {
                throw new ServiceException("An error occurred while fetching business payment credentials.", ex);
            }
        }

        public async Task<BusinessPaymentCredentialViewModel> GetBusinessPaymentCredentialByIdAsync(int id)
        {
            try
            {
                var businessPaymentCredential = await _businessPaymentCredentialRepository.GetByIdAsync(id);

                if (businessPaymentCredential == null)
                {
                    throw new NotFoundException($"Business payment credential with ID {id} was not found.");
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
                throw new ServiceException($"An error occurred while fetching business payment credential with ID {id}.", ex);
            }
        }

        public async Task<BusinessPaymentCredentialViewModel> CreateBusinessPaymentCredentialAsync(BusinessPaymentCredentialCreateViewModel model)
        {
            try
            {
                var businessPaymentCredential = _mapper.Map<BusinessPaymentCredential>(model);
                await _businessPaymentCredentialRepository.CreateAsync(businessPaymentCredential);
                return _mapper.Map<BusinessPaymentCredentialViewModel>(businessPaymentCredential);
            }
            catch (Exception ex)
            {
                throw new ServiceException("An error occurred while creating the business payment credential.", ex);
            }
        }

        public async Task UpdateBusinessPaymentCredentialAsync(BusinessPaymentCredentialUpdateViewModel model)
        {
            try
            {
                var businessPaymentCredential = await _businessPaymentCredentialRepository.GetByIdAsync(model.Id);
                businessPaymentCredential = _mapper.Map(model, businessPaymentCredential);
                await _businessPaymentCredentialRepository.UpdateAsync(businessPaymentCredential);
            }
            catch (Exception ex)
            {
                throw new ServiceException($"An error occurred while updating business payment credential with ID {model.Id}.", ex);
            }
        }

        public async Task DeleteBusinessPaymentCredentialAsync(int id)
        {
            try
            {
                var businessPaymentCredential = await _businessPaymentCredentialRepository.GetByIdAsync(id);
                await _businessPaymentCredentialRepository.DeleteAsync(businessPaymentCredential);
            }
            catch (Exception ex)
            {
                throw new ServiceException($"An error occurred while deleting business payment credential with ID {id}.", ex);
            }
        }
    }
}
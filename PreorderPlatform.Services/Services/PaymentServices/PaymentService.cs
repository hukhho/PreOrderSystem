using AutoMapper;
using PreorderPlatform.Entity.Models;
using PreorderPlatform.Entity.Repositories.PaymentRepositories;
using PreorderPlatform.Service.ViewModels.Payment;
using PreorderPlatform.Service.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PreorderPlatform.Service.Utility;
using Microsoft.EntityFrameworkCore;
using PreorderPlatform.Service.Utility.Pagination;
using PreorderPlatform.Service.Enum;
using PreorderPlatform.Service.ViewModels.Payment.Response;

namespace PreorderPlatform.Service.Services.PaymentServices
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;

        public PaymentService(IPaymentRepository paymentRepository, IMapper mapper)
        {
            _paymentRepository = paymentRepository;
            _mapper = mapper;
        }

        public async Task<List<PaymentViewModel>> GetPaymentsAsync()
        {
            try
            {
                var payments = await _paymentRepository.GetAllAsync();
                return _mapper.Map<List<PaymentViewModel>>(payments);
            }
            catch (Exception ex)
            {
                throw new ServiceException("An error occurred while fetching payments.", ex);
            }
        }

        public async Task<PaymentByIdResponse> GetPaymentByIdAsync(int id)
        {
            try
            {
                var payment = await _paymentRepository.GetPaymentByIdAsync(id);

                if (payment == null)
                {
                    throw new NotFoundException($"Payment with ID {id} was not found.");
                }

                return _mapper.Map<PaymentByIdResponse>(payment);
            }
            catch (NotFoundException)
            {
                // Rethrow NotFoundException to be handled by the caller
                throw;
            }
            catch (Exception ex)
            {
                throw new ServiceException($"An error occurred while fetching payment with ID {id}.", ex);
            }
        }

        public async Task<PaymentViewModel> CreatePaymentAsync(PaymentCreateViewModel model)
        {
            try
            {
                var payment = _mapper.Map<Payment>(model);
                await _paymentRepository.CreateAsync(payment);
                return _mapper.Map<PaymentViewModel>(payment);
            }
            catch (Exception ex)
            {
                throw new ServiceException("An error occurred while creating the payment.", ex);
            }
        }

        public async Task UpdatePaymentAsync(PaymentUpdateViewModel model)
        {
            try
            {
                var payment = await _paymentRepository.GetByIdAsync(model.Id);
                payment = _mapper.Map(model, payment);
                await _paymentRepository.UpdateAsync(payment);
            }
            catch (Exception ex)
            {
                throw new ServiceException($"An error occurred while updating payment with ID {model.Id}.", ex);
            }
        }

        public async Task DeletePaymentAsync(int id)
        {
            try
            {
                var payment = await _paymentRepository.GetByIdAsync(id);
                await _paymentRepository.DeleteAsync(payment);
            }
            catch (Exception ex)
            {
                throw new ServiceException($"An error occurred while deleting payment with ID {id}.", ex);
            }
        }

        public async Task<(IList<PaymentViewModel> payments, int totalItems)> GetAsync(PaginationParam<PaymentEnum.PaymentSort> paginationModel, PaymentSearchRequest filterModel)
        {
            try
            {
                var query = _paymentRepository.Table;

                query = query.GetWithSearch(filterModel); //search

                // Calculate the total number of items before applying pagination
                int totalItems = await query.CountAsync();

                query = query.GetWithSorting(paginationModel.SortKey.ToString(), paginationModel.SortOrder) //sort
                            .GetWithPaging(paginationModel.Page, paginationModel.PageSize);  // pagination

                var paymentList = await query.ToListAsync(); // Call ToListAsync here

                // Map the paymentList to a list of PaymentViewModel objects
                var result = _mapper.Map<List<PaymentViewModel>>(paymentList);

                return (result, totalItems);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception " + ex.Message);
                throw new ServiceException("An error occurred while fetching payments.", ex);
            }
        }
        
    }
}
using AutoMapper;
using PreorderPlatform.Entity.Models;
using PreorderPlatform.Entity.Repositories.OrderRepositories;
using PreorderPlatform.Service.Services.OrderServices;
using PreorderPlatform.Service.ViewModels.Order;
using PreorderPlatform.Service.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PreorderPlatform.Service.ViewModels.Order.Response;
using PreorderPlatform.Service.Utility.Pagination;
using PreorderPlatform.Service.Enum;
using PreorderPlatform.Service.ViewModels.Order.Request;
using PreorderPlatform.Service.Utility;
using Microsoft.EntityFrameworkCore;

namespace PreorderPlatform.Service.Services.OrderServices
{
    internal class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<List<OrderViewModel>> GetOrdersAsync()
        {
            try
            {
                var orders = await _orderRepository.GetAllAsync();
                return _mapper.Map<List<OrderViewModel>>(orders);
            }
            catch (Exception ex)
            {
                throw new ServiceException("An error occurred while fetching orders.", ex);
            }
        }

        public async Task<OrderViewModel> GetOrderByIdAsync(int id)
        {
            try
            {
                var order = await _orderRepository.GetByIdAsync(id);

                if (order == null)
                {
                    throw new NotFoundException($"Order with ID {id} was not found.");
                }

                return _mapper.Map<OrderViewModel>(order);
            }
            catch (NotFoundException)
            {
                // Rethrow NotFoundException to be handled by the caller
                throw;
            }
            catch (Exception ex)
            {
                throw new ServiceException($"An error occurred while fetching order with ID {id}.", ex);
            }
        }

        public async Task<OrderViewModel> CreateOrderAsync(OrderCreateViewModel model)
        {
            try
            {
                var order = _mapper.Map<Order>(model);
                await _orderRepository.CreateAsync(order);
                return _mapper.Map<OrderViewModel>(order);
            }
            catch (Exception ex)
            {
                throw new ServiceException("An error occurred while creating the order.", ex);
            }
        }

        public async Task UpdateOrderAsync(OrderUpdateViewModel model)
        {
            try
            {
                var order = await _orderRepository.GetByIdAsync(model.Id);
                order = _mapper.Map(model, order);
                await _orderRepository.UpdateAsync(order);
            }
            catch (Exception ex)
            {
                throw new ServiceException($"An error occurred while updating order with ID {model.Id}.", ex);
            }
        }

        public async Task DeleteOrderAsync(int id)
        {
            try
            {
                var order = await _orderRepository.GetByIdAsync(id);
                await _orderRepository.DeleteAsync(order);
            }
            catch (Exception ex)
            {
                throw new ServiceException($"An error occurred while deleting order with ID {id}.", ex);
            }
        }

        async Task<IList<OrderResponse>> IOrderService.GetAsync(PaginationParam<OrderEnum.OrderSort> paginationModel, OrderSearchRequest filterModel)
        {
            try
            {
                var startDate = filterModel.StartDateInRange;
                var endDate = filterModel.EndDateInRange;
                filterModel.StartDateInRange = null;
                filterModel.EndDateInRange = null;

                var query = _orderRepository.Table;

                query = query.GetWithSearch(filterModel);


                query = query.FilterOrderByDate(o => o.CreatedAt, startDate, endDate);
                query = query.GetWithSorting(paginationModel.SortKey.ToString(), paginationModel.SortOrder)
                    .GetWithPaging(paginationModel.Page, paginationModel.PageSize);

                var orderList = await query.ToListAsync();
                var res = _mapper.Map<List<OrderResponse>>(orderList);

                return res;
            } catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
                throw new ServiceException("An error occurred while fetching orders.", e);
            }
        }
    }
}
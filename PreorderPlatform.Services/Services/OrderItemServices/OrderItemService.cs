﻿using AutoMapper;
using PreorderPlatform.Entity.Models;
using PreorderPlatform.Entity.Repositories.OrderItemRepositories;
using PreorderPlatform.Service.ViewModels.OrderItem;
using PreorderPlatform.Service.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PreorderPlatform.Service.Utility;
using Microsoft.EntityFrameworkCore;
using PreorderPlatform.Service.Utility.Pagination;
using PreorderPlatform.Service.Enum;
using PreorderPlatform.Service.ViewModels.OrderItem.Response;

namespace PreorderPlatform.Service.Services.OrderItemServices
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IMapper _mapper;

        public OrderItemService(IOrderItemRepository orderItemRepository, IMapper mapper)
        {
            _orderItemRepository = orderItemRepository;
            _mapper = mapper;
        }

        public async Task<List<OrderItemViewModel>> GetOrderItemsAsync()
        {
            try
            {
                var orderItems = await _orderItemRepository.GetAllAsync();
                return _mapper.Map<List<OrderItemViewModel>>(orderItems);
            }
            catch (Exception ex)
            {
                throw new ServiceException("An error occurred while fetching order items.", ex);
            }
        }

        public async Task<OrderItemByIdResponse> GetOrderItemByIdAsync(int id)
        {
            try
            {
                var orderItem = await _orderItemRepository.GetOrderItemByIdAsync(id);

                if (orderItem == null)
                {
                    throw new NotFoundException($"Order item with ID {id} was not found.");
                }

                return _mapper.Map<OrderItemByIdResponse>(orderItem);
            }
            catch (NotFoundException)
            {
                // Rethrow NotFoundException to be handled by the caller
                throw;
            }
            catch (Exception ex)
            {
                throw new ServiceException($"An error occurred while fetching order item with ID {id}.", ex);
            }
        }

        public async Task<OrderItemViewModel> CreateOrderItemAsync(OrderItemCreateViewModel model)
        {
            try
            {
                var orderItem = _mapper.Map<OrderItem>(model);
                await _orderItemRepository.CreateAsync(orderItem);
                return _mapper.Map<OrderItemViewModel>(orderItem);
            }
            catch (Exception ex)
            {
                throw new ServiceException("An error occurred while creating the order item.", ex);
            }
        }

        public async Task UpdateOrderItemAsync(OrderItemUpdateViewModel model)
        {
            try
            {
                var orderItem = await _orderItemRepository.GetByIdAsync(model.Id);
                orderItem = _mapper.Map(model, orderItem);
                await _orderItemRepository.UpdateAsync(orderItem);
            }
            catch (Exception ex)
            {
                throw new ServiceException($"An error occurred while updating order item with ID {model.Id}.", ex);
            }
        }

        public async Task DeleteOrderItemAsync(int id)
        {
            try
            {
                var orderItem = await _orderItemRepository.GetByIdAsync(id);
                await _orderItemRepository.DeleteAsync(orderItem);
            }
            catch (Exception ex)
            {
                throw new ServiceException($"An error occurred while deleting order item with ID {id}.", ex);
            }
        }
        public async Task<(IList<OrderItemViewModel> orderItems, int totalItems)> GetAsync(PaginationParam<OrderItemEnum.OrderItemSort> paginationModel, OrderItemSearchRequest filterModel)
        {
            try
            {
                var query = _orderItemRepository.Table;

                query = query.GetWithSearch(filterModel); //search

                // Calculate the total number of items before applying pagination
                int totalItems = await query.CountAsync();

                query = query.GetWithSorting(paginationModel.SortKey.ToString(), paginationModel.SortOrder) //sort
                            .GetWithPaging(paginationModel.Page, paginationModel.PageSize);  // pagination

                var orderItemList = await query.ToListAsync(); // Call ToListAsync here

                // Map the orderItemList to a list of OrderItemViewModel objects
                var result = _mapper.Map<List<OrderItemViewModel>>(orderItemList);

                return (result, totalItems);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception " + ex.Message);
                throw new ServiceException("An error occurred while fetching order items.", ex);
            }
        }
        
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PreorderPlatform.Entity.Data;
using PreorderPlatform.Entity.Models;
using PreorderPlatform.Entity.Repositories.CampaignDetailRepositories;
using PreorderPlatform.Entity.Repositories.OrderRepositories;
using PreorderPlatform.Service.Enum;
using PreorderPlatform.Service.Exceptions;
using PreorderPlatform.Service.Helpers;
using PreorderPlatform.Service.Services.OrderServices;
using PreorderPlatform.Service.Utility;
using PreorderPlatform.Service.Utility.Pagination;
using PreorderPlatform.Service.ViewModels.Order;
using PreorderPlatform.Service.ViewModels.Order.Request;
using PreorderPlatform.Service.ViewModels.Order.Response;

namespace PreorderPlatform.Service.Services.OrderServices
{
    internal class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICampaignDetailRepository _campaignDetailRepository;
        private readonly IMapper _mapper;
        private readonly PreOrderSystemContext _context;


        public OrderService(PreOrderSystemContext context, IOrderRepository orderRepository, ICampaignDetailRepository campaignDetailRepositor, IMapper mapper)
        {
            this._context = context;
            _orderRepository = orderRepository;
            _mapper = mapper;
            _campaignDetailRepository = campaignDetailRepositor;
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

        public async Task<OrderByIdResponse> GetOrderByIdAsync(Guid id)
        {
            try
            {
                var order = await _orderRepository.GetOrderByIdAsync(id);

                if (order == null)
                {
                    throw new NotFoundException($"Order with ID {id} was not found.");
                }

                return _mapper.Map<OrderByIdResponse>(order);
            }
            catch (NotFoundException)
            {
                // Rethrow NotFoundException to be handled by the caller
                throw;
            }
            catch (Exception ex)
            {
                throw new ServiceException(
                    $"An error occurred while fetching order with ID {id}.",
                    ex
                );
            }
        }

        public async Task<OrderViewModel> CreateOrderAsync(Guid userId, OrderCreateViewModel model)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var order = _mapper.Map<Order>(model);

                // Check if the order has exactly one payment
                if (order.Payments.Count != 1)
                {
                    throw new ServiceException(
                        "Only one payment can be added when creating an order."
                    );
                }
                // Check if the order has one item
                if (order.OrderItems.Count < 1)
                {
                    throw new ServiceException(
                        "At least order item can be added when creating an order."
                    );
                }

                order.IsDeposited = false;

                order.Payments.Single().PayedAt = DateTimeUtcPlus7.Now;

                order.Payments.Single().Status = "Test Ne";

                // Iterate over each order item and update the corresponding CampaignDetail entity
                int totalQuantity = 0;
                decimal totalPrice = 0;
                decimal requiredDepositAmount = 0;
                order.Status = "Init";
                order.UserId = userId;


                // Get the list of CampaignDetailIds from the order items
                var campaignDetailIds = order.OrderItems.Select(item => item.CampaignDetailId);

                // Check if all campaig
                // n details belong to the business
                var areAllCampaignDetailsInBusiness = await _campaignDetailRepository.AreAllCampaignDetailsInBusinessAsync(campaignDetailIds);

                if (!areAllCampaignDetailsInBusiness)
                {
                    // Handle the case when not all campaign details belong to the business
                    throw new Exception("One or more campaign details do not belong to the business.");
                }

                List<CampaignDetail> campaignDetailsToUpdate = new List<CampaignDetail>();

                foreach (var item in order.OrderItems)
                {
                    // Fetch the corresponding CampaignDetail entity
                    var campaignDetail = await _campaignDetailRepository.GetByIdAsync(item.CampaignDetailId);

                    if (campaignDetail == null)
                    {
                        throw new NotFoundException($"Campaign detail ID {item.CampaignDetailId} not found.");
                    }

                    if (item.Quantity >= campaignDetail.AllowedQuantity - campaignDetail.TotalOrdered)
                    {
                        throw new ServiceException("Quantity not enough!");
                    }

                    // Increment the TotalOrdered field by the quantity of the current item
                    campaignDetail.TotalOrdered += item.Quantity;

                    // Set the price of the current item to the price of the CampaignDetail entity
                    item.UnitPrice = campaignDetail.Price;
                    totalQuantity += item.Quantity;

                    totalPrice += item.Quantity * item.UnitPrice;

                    // Add the campaignDetail entity to the list
                    campaignDetailsToUpdate.Add(campaignDetail);
                }

                // Now, update all entities at once
                await _campaignDetailRepository.UpdateMultiAsync(campaignDetailsToUpdate);

                decimal shippingRate = 30000;

                order.ShippingPrice = shippingRate;
                order.ShippingStatus = "NOT READY";
                order.ShippingCode = "NULL";
                order.TotalPrice += totalPrice;

                requiredDepositAmount = 0.5m * totalPrice;
                order.RequiredDepositAmount = requiredDepositAmount;

                order.TotalPrice += shippingRate;

                order.CreatedAt = DateTimeUtcPlus7.Now;

                await _orderRepository.CreateAsync(order);


                await transaction.CommitAsync();

                return _mapper.Map<OrderViewModel>(order);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CreateOrderAsync {ex}");
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
                throw new ServiceException(
                    $"An error occurred while updating order with ID {model.Id}.",
                    ex
                );
            }
        }

        public async Task DeleteOrderAsync(Guid id)
        {
            try
            {
                var order = await _orderRepository.GetByIdAsync(id);
                await _orderRepository.DeleteAsync(order);
            }
            catch (Exception ex)
            {
                throw new ServiceException(
                    $"An error occurred while deleting order with ID {id}.",
                    ex
                );
            }
        }

        public async Task<(IList<OrderResponse> orders, int totalItems)> GetAsync(
            PaginationParam<OrderEnum.OrderSort> paginationModel,
            OrderSearchRequest filterModel
        )
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

                // Calculate the total number of items before applying pagination
                int totalItems = await query.CountAsync();

                query = query
                    .GetWithSorting(paginationModel.SortKey.ToString(), paginationModel.SortOrder)
                    .GetWithPaging(paginationModel.Page, paginationModel.PageSize);

                var orderList = await query.ToListAsync();
                var res = _mapper.Map<List<OrderResponse>>(orderList);

                return (res, totalItems);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
                throw new ServiceException("An error occurred while fetching orders.", e);
            }
        }
    }
}

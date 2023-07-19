using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using PreorderPlatform.Entity.Repositories.BusinessRepositories;
using PreorderPlatform.Entity.Repositories.OrderRepositories;
using PreorderPlatform.Service.Exceptions;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PreorderPlatform.Service.Services.AuthorizationService.Order
{
    public class MustBeOrderAccessHandler : AuthorizationHandler<MustBeOrderAccessRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IServiceProvider _serviceProvider;

        public MustBeOrderAccessHandler(
            IHttpContextAccessor httpContextAccessor,
            IServiceProvider serviceProvider
        )
        {
            _httpContextAccessor = httpContextAccessor;
            _serviceProvider = serviceProvider;
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            MustBeOrderAccessRequirement requirement
        )
        {
            if (!context.User.HasClaim(c => c.Type == ClaimTypes.NameIdentifier))
            {
                throw new ArgumentException("User does not have NameIdentifier claim.");
            }

            // Succeed requirement for users with the Admin role
            if (context.User.IsInRole("ADMIN"))
            {
                context.Succeed(requirement);
                return;
            }

           

            var httpContext = _httpContextAccessor.HttpContext;

            var routeData = _httpContextAccessor.HttpContext.GetRouteData();

            string orderIdString;

            if (routeData.Values["orderId"] is string orderIdFromRoute)
            {
                orderIdString = orderIdFromRoute;
            }
            else if (routeData.Values["id"] is string idString)
            {
                orderIdString = idString;
            }
            else
            {
                throw new NotFoundException("Order ID not found in route data.");
            }

            if (!Guid.TryParse(orderIdString, out var orderId))
            {
                throw new ArgumentException("Order ID is not a valid Guid.");
            }

            using (var scope = _serviceProvider.CreateScope())
            {
                var orderRepo = scope.ServiceProvider.GetRequiredService<IOrderRepository>();

                var userId = Guid.Parse(
                    context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value
                );

                var userEmail = context.User.FindFirst(c => c.Type == ClaimTypes.Name).Value;

                // Check if order exists
                if (!await orderRepo.IsExistsByGuid(orderId))
                {
                    throw new NotFoundException($"Order {orderId} does not exist.");
                }

                // Check if user is customer of the order
                if (await orderRepo.IsUserCanAccessOrder(userId, orderId))
                {
                    context.Succeed(requirement);
                }
                else
                {
                    throw new AuthorizationException(
                        $"User {userEmail} does not have access to order {orderId}."
                    );
                }
            }

            return;
        }
    }
}
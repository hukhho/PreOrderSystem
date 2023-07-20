using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using PreorderPlatform.Entity.Repositories.PaymentRepositories;
using PreorderPlatform.Service.Exceptions;
using PreorderPlatform.Service.Services.AuthorizationService.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PreorderPlatform.Service.Services.AuthorizationService.Payment
{
    public class MustBePaymentAccessHandler : AuthorizationHandler<MustBePaymentAccessRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IServiceProvider _serviceProvider;

        public MustBePaymentAccessHandler(
            IHttpContextAccessor httpContextAccessor,
            IServiceProvider serviceProvider
        )
        {
            _httpContextAccessor = httpContextAccessor;
            _serviceProvider = serviceProvider;
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            MustBePaymentAccessRequirement requirement
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

            string paymentIdString;

            if (routeData.Values["paymentId"] is string paymentIdFromRoute)
            {
                paymentIdString = paymentIdFromRoute;
            }
            else if (routeData.Values["id"] is string idString)
            {
                paymentIdString = idString;
            }
            else
            {
                throw new NotFoundException("Payment ID not found in route data.");
            }

            if (!Guid.TryParse(paymentIdString, out var paymentId))
            {
                throw new ArgumentException("Payment ID is not a valid Guid.");
            }

            using (var scope = _serviceProvider.CreateScope())
            {
                var paymentRepo = scope.ServiceProvider.GetRequiredService<IPaymentRepository>();

                var userId = Guid.Parse(
                    context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value
                );

                var userEmail = context.User.FindFirst(c => c.Type == ClaimTypes.Name).Value;

                // Check if payment exists
                if (!await paymentRepo.IsExistsByGuid(paymentId))
                {
                    throw new NotFoundException($"Payment {paymentId} does not exist.");
                }

                // Check if user is owner of the payment
                if (await paymentRepo.IsUserCanAccessPayment(userId, paymentId))
                {
                    context.Succeed(requirement);
                }
                else
                {
                    throw new AuthorizationException(
                        $"User {userEmail} does not have access to payment {paymentId}."
                    );
                }
            }
        }
    }
}

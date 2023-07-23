using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using PreOrderPlatform.Entity.Repositories.BusinessRepositories;
using PreOrderPlatform.Service.Services.Exceptions;

namespace PreOrderPlatform.Service.Services.AuthorizationService.Business
{
    public class MustBeBusinessOwnerHandler : AuthorizationHandler<MustBusinessOwnerRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IServiceProvider _serviceProvider;

        public MustBeBusinessOwnerHandler(
            IHttpContextAccessor httpContextAccessor,
            IServiceProvider serviceProvider
        )
        {
            _httpContextAccessor = httpContextAccessor;
            _serviceProvider = serviceProvider;
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            MustBusinessOwnerRequirement requirement
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

            // If user doesn't have the BUSINESS_OWNER role, then don't allow access
            if (!context.User.IsInRole("BUSINESS_OWNER"))
            {
                throw new AuthorizationException("User is not a business owner.");
            }

            var httpContext = _httpContextAccessor.HttpContext;

            var routeData = _httpContextAccessor.HttpContext.GetRouteData();

            string businessIdString;

            if (routeData.Values["businessId"] is string businessIdFromRoute)
            {
                businessIdString = businessIdFromRoute;
            }
            else if (routeData.Values["id"] is string idString)
            {
                businessIdString = idString;
            }
            else
            {
                throw new NotFoundException("Business ID not found in route data.");
            }

            if (!Guid.TryParse(businessIdString, out var businessId))
            {
                throw new ArgumentException("Business ID is not a valid Guid.");
            }

            using (var scope = _serviceProvider.CreateScope())
            {
                var businessRepo = scope.ServiceProvider.GetRequiredService<IBusinessRepository>();

                var userId = Guid.Parse(
                    context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value
                );

                var userEmail = context.User.FindFirst(c => c.Type == ClaimTypes.Name).Value;

                // Check if business exists
                if (!await businessRepo.IsExistsByGuid(businessId))
                {
                    throw new NotFoundException($"Business {businessId} does not exist.");
                }

                // Check if user is owner
                if (await businessRepo.IsUserOwnerOfBusiness(userId, businessId))
                {
                    context.Succeed(requirement);
                }
                else
                {
                    throw new AuthorizationException(
                        $"User {userEmail} is not the owner of business {businessId}."
                    );
                }
            }

            return;
        }
    }
}

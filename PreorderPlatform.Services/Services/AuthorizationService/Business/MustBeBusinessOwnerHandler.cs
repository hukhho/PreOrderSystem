using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using PreorderPlatform.Entity.Repositories.BusinessRepositories;
using PreorderPlatform.Service.Exceptions;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PreorderPlatform.Service.Services.AuthorizationService.Business
{
    public class MustBeBusinessOwnerHandler : AuthorizationHandler<MustBusinessOwnerRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IServiceProvider _serviceProvider;

        public MustBeBusinessOwnerHandler(IHttpContextAccessor httpContextAccessor, IServiceProvider serviceProvider)
        {
            _httpContextAccessor = httpContextAccessor;
            _serviceProvider = serviceProvider;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, MustBusinessOwnerRequirement requirement)
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
            var routeData = httpContext.GetRouteData();
            if (routeData.Values["id"] is not string businessIdString || !Guid.TryParse(businessIdString, out var businessId))
            {
                throw new NotFoundException("Business ID not found or not a valid Guid.");
            }

            using (var scope = _serviceProvider.CreateScope())
            {
                var businessRepo = scope.ServiceProvider.GetRequiredService<IBusinessRepository>();

                var userId = Guid.Parse(context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);

                var userEmail = context.User.FindFirst(c => c.Type == ClaimTypes.Name).Value;

                if (await businessRepo.IsUserOwnerOfBusiness(userId, businessId))
                {
                    context.Succeed(requirement);
                }
                else
                {
                    throw new AuthorizationException($"User {userEmail} is not the owner of business {businessId}.");
                }
            }

            return;
        }
    }
}
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using PreOrderPlatform.Entity.Repositories.CampaignRepositories;
using PreOrderPlatform.Service.Services.Exceptions;

namespace PreOrderPlatform.Service.Services.AuthorizationService.Campaign
{
    public class MustBeCampaignOwnerHandler : AuthorizationHandler<MustCampaignOwnerRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IServiceProvider _serviceProvider;

        public MustBeCampaignOwnerHandler(IHttpContextAccessor httpContextAccessor, IServiceProvider serviceProvider)
        {
            _httpContextAccessor = httpContextAccessor;
            _serviceProvider = serviceProvider;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, MustCampaignOwnerRequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Type == ClaimTypes.NameIdentifier))
            {
                throw new ArgumentException("User does not have NameIdentifier claim.");
            }

            // If user doesn't have the BUSINESS_OWNER and BUSINESS_STAFF role, then don't allow access
            if (!context.User.IsInRole("BUSINESS_OWNER") && !context.User.IsInRole("BUSINESS_STAFF"))
            {
                throw new AuthorizationException("User is not a business owner or staff.");
            }

            var httpContext = _httpContextAccessor.HttpContext;
            var routeData = httpContext.GetRouteData();
            if (routeData.Values["campaignId"] is not string idString || !Guid.TryParse(idString, out var campaignId))
            {
                Console.WriteLine($"campaignId ID not found or not a valid Guid. {routeData.Values["campaignId"]}");
                throw new NotFoundException("campaignId ID not found or not a valid Guid.");
            }
            Console.WriteLine($"campaignId ID {routeData.Values["campaignId"]}");

            using (var scope = _serviceProvider.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<ICampaignRepository>();

                var userId = Guid.Parse(context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);

                var userEmail = context.User.FindFirst(c => c.Type == ClaimTypes.Name).Value;

                if (await repo.IsOwnerOrStaff(userId, campaignId))
                {
                    context.Succeed(requirement);
                }
                else
                {
                    throw new AuthorizationException($"User {userEmail} is not the owner or staff of business that hold campaign {campaignId}.");
                }
            }

            return;
        }
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using PreorderPlatform.Entity.Repositories.BusinessRepositories;
using PreorderPlatform.Entity.Repositories.CampaignRepositories;
using PreorderPlatform.Service.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PreorderPlatform.Service.Services.AuthorizationService.Campaign
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
            if (routeData.Values["id"] is not string idString || !int.TryParse(idString, out var campaignId))
            {
                throw new NotFoundException("Business ID not found or not a valid integer.");
            }

            using (var scope = _serviceProvider.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<ICampaignRepository>();

                var userId = int.Parse(context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);

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
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using PreOrderPlatform.Entity.Repositories.ProductRepositories;
using PreOrderPlatform.Service.Enums;
using PreOrderPlatform.Service.Services.Exceptions;

namespace PreOrderPlatform.Service.Services.AuthorizationService.Product
{
    public class MustBeProductAccessHandler :
        AuthorizationHandler<MustBeProductAccessRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IServiceProvider _serviceProvider;

        public MustBeProductAccessHandler(
            IHttpContextAccessor httpContextAccessor,
            IServiceProvider serviceProvider)
        {
            _httpContextAccessor = httpContextAccessor;
            _serviceProvider = serviceProvider;
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            MustBeProductAccessRequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Type == ClaimTypes.NameIdentifier))
            {
                throw new ArgumentException("User does not have NameIdentifier claim.");
            }

            var httpContext = _httpContextAccessor.HttpContext;
            var routeData = httpContext.GetRouteData();
            var productId = Guid.Parse(routeData.Values["productId"].ToString());

            if (context.User.IsInRole(UserRole.ADMIN.ToString()))
            {
                context.Succeed(requirement);
                return;
            }

            using (var scope = _serviceProvider.CreateScope())
            {
                var productRepo = scope.ServiceProvider.
                    GetRequiredService<IProductRepository>();

                var userId = Guid.Parse(context.User.FindFirst(
                    c => c.Type == ClaimTypes.NameIdentifier).Value);

                if (await productRepo.IsUserCanAccessProduct(userId, productId))
                {
                    context.Succeed(requirement);
                }
                else
                {
                    throw new AuthorizationException(
                        "User does not have access to this product");
                }
            }
        }
    }
}
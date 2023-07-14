using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PreorderPlatform.Entity;
using PreorderPlatform.Entity.Repositories;
using PreorderPlatform.Service.Services;
using PreorderPlatform.Service.Services.AuthorizationService.Business;
using PreorderPlatform.Service.Services.AuthService;
using PreorderPlatform.Service.Services.BusinessPaymentCredentialServices;
using PreorderPlatform.Service.Services.BusinessServices;
using PreorderPlatform.Service.Services.CampaignDetailServices;
using PreorderPlatform.Service.Services.CampaignServices;
using PreorderPlatform.Service.Services.CategoryServices;
using PreorderPlatform.Service.Services.OrderItemServices;
using PreorderPlatform.Service.Services.OrderServices;
using PreorderPlatform.Service.Services.PaymentServices;
using PreorderPlatform.Service.Services.ProductServices;
using PreorderPlatform.Service.Services.RoleServices;

using PreorderPlatform.Service.Services.UserServices;
using PreorderPlatform.Service.ViewModels.AutoMapperProfile;

namespace PreorderPlatform.Service
{
    public static class ModuleRegister
    {
        public static void RegisterBusiness(this IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterRepository();
            services.RegisterService();
            services.ConfigureDBContext(configuration);
            services.ConfigAutoMapper();
            services.RegisterPolicies();
            services.AddHttpContextAccessor();


        }


        public static void RegisterPolicies(this IServiceCollection services)
        {
            //services.AddSingleton<IServiceProvider>(services);
            services.AddSingleton<IAuthorizationHandler, MustBeBusinessOwnerHandler>();
            services.AddSingleton<IAuthorizationRequirement, MustBeBusinessOwnerRequirement>();
            services.AddAuthorization(options =>
            {
                // Add policies here. Here's an example:
                options.AddPolicy("MustBeBusinessOwner", policy =>
                         policy.Requirements.Add(new MustBeBusinessOwnerRequirement()));


            });
        }


        public static void RegisterService(this IServiceCollection services)
        {
            services.AddScoped<IBusinessService, BusinessService>();
            services.AddScoped<IBusinessPaymentCredentialService, BusinessPaymentCredentialService>();
            services.AddScoped<ICampaignService, CampaignService>();
            services.AddScoped<ICampaignDetailService, CampaignDetailService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderItemService, OrderItemService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IJwtService, JwtService>();

        }

        public static void ConfigAutoMapper(this IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.ConfigBusinessMapper();
                mc.ConfigBusinessPaymentCredentialMapper();
                mc.ConfigCampaignDetailMapper();
                mc.ConfigCampaignMapper();
                mc.ConfigCategoryMapper();
                mc.ConfigOrderItemMapper();
                mc.ConfigOrderMapper();
                mc.ConfigPaymentMapper();
                mc.ConfigProductMapper();
                mc.ConfigRoleMapper();
                mc.ConfigUserMapper();
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}

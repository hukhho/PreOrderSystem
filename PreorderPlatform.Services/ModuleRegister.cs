using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PreOrderPlatform.Entity;
using PreOrderPlatform.Service.Services.AuthorizationService.Business;
using PreOrderPlatform.Service.Services.AuthorizationService.BusinessPaymentCredential;
using PreOrderPlatform.Service.Services.AuthorizationService.Campaign;
using PreOrderPlatform.Service.Services.AuthorizationService.Order;
using PreOrderPlatform.Service.Services.AuthorizationService.Payment;
using PreOrderPlatform.Service.Services.AuthorizationService.Product;
using PreOrderPlatform.Service.Services.AuthService;
using PreOrderPlatform.Service.Services.BusinessPaymentCredentialServices;
using PreOrderPlatform.Service.Services.BusinessServices;
using PreOrderPlatform.Service.Services.CampaignDetailServices;
using PreOrderPlatform.Service.Services.CampaignServices;
using PreOrderPlatform.Service.Services.CategoryServices;
using PreOrderPlatform.Service.Services.OrderItemServices;
using PreOrderPlatform.Service.Services.OrderServices;
using PreOrderPlatform.Service.Services.PaymentServices;
using PreOrderPlatform.Service.Services.ProductServices;
using PreOrderPlatform.Service.Services.RoleServices;
using PreOrderPlatform.Service.Services.SendMailServices;
using PreOrderPlatform.Service.Services.UserServices;
using PreOrderPlatform.Service.ViewModels.AutoMapperProfile;

namespace PreOrderPlatform.Service
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

            services.AddSingleton<IAuthorizationRequirement, MustBusinessOwnerRequirement>();
            services.AddSingleton<IAuthorizationHandler, MustBeBusinessOwnerHandler>();

            services.AddSingleton<IAuthorizationRequirement, MustBusinessPaymentCredentialOwnerRequirement>();
            services.AddSingleton<IAuthorizationHandler, MustBeBusinessPaymentCredentialOwnerHandler>();


            services.AddSingleton<IAuthorizationRequirement, MustCampaignOwnerRequirement>();
            services.AddSingleton<IAuthorizationHandler, MustBeCampaignOwnerHandler>();

            services.AddSingleton<IAuthorizationRequirement, MustBeOrderAccessRequirement>();
            services.AddSingleton<IAuthorizationHandler, MustBeOrderAccessHandler>();


            services.AddSingleton<IAuthorizationRequirement, MustBePaymentAccessRequirement>();
            services.AddSingleton<IAuthorizationHandler, MustBePaymentAccessHandler>();


            services.AddSingleton<IAuthorizationRequirement, MustBeProductAccessRequirement>();
            services.AddSingleton<IAuthorizationHandler, MustBeProductAccessHandler>();


            services.AddAuthorization(options =>
            {
                // Add policies here. Here's an example:
                options.AddPolicy("MustBeBusinessOwner", policy =>
                         policy.Requirements.Add(new MustBusinessOwnerRequirement()));
                options.AddPolicy("MustBeCampaignOwnerOrStaff", policy =>
                         policy.Requirements.Add(new MustCampaignOwnerRequirement()));
                options.AddPolicy("MustBeBusinessPaymentCredentialOwner", policy =>
                    policy.Requirements.Add(new MustBusinessPaymentCredentialOwnerRequirement()));
                options.AddPolicy("MustBeOrderAccess", policy =>
                  policy.Requirements.Add(new MustBeOrderAccessRequirement()));
                options.AddPolicy("MustBePaymentAccess", policy =>
                    policy.Requirements.Add(new MustBePaymentAccessRequirement()));
                options.AddPolicy("MustBeProductAccess", policy =>
                    policy.Requirements.Add(new MustBeProductAccessRequirement()));
               
            });
        }


        public static void RegisterService(this IServiceCollection services)
        {
            services.AddScoped<IEmailService, EmailService>();
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

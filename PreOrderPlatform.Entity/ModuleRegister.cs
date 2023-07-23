using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PreOrderPlatform.Entity.Data;
using PreOrderPlatform.Entity.Models;
using PreOrderPlatform.Entity.Repositories.BusinessPaymentCredentialRepositories;
using PreOrderPlatform.Entity.Repositories.BusinessRepositories;
using PreOrderPlatform.Entity.Repositories.CampaignDetailRepositories;
using PreOrderPlatform.Entity.Repositories.CampaignRepositories;
using PreOrderPlatform.Entity.Repositories.CategoryRepositories;
using PreOrderPlatform.Entity.Repositories.OrderItemRepositories;
using PreOrderPlatform.Entity.Repositories.OrderRepositories;
using PreOrderPlatform.Entity.Repositories.PaymentRepositories;
using PreOrderPlatform.Entity.Repositories.ProductRepositories;
using PreOrderPlatform.Entity.Repositories.RoleRepositories;
using PreOrderPlatform.Entity.Repositories.UserRepositories;

namespace PreOrderPlatform.Entity
{
    public static class ModuleRegister
    {
        public static void ConfigureDBContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PreOrderSystemContext>(options
    => options.UseSqlServer(configuration.GetConnectionString("PreOrderSystem")));
        }

        public static void RegisterRepository(this IServiceCollection services)
        {
            services.AddScoped<IDbInitializer, DbInitializer>();
            services.AddScoped<IBusinessRepository, BusinessRepository>();
            services.AddScoped<IBusinessPaymentCredentialRepository, BusinessPaymentCredentialRepository>();
            services.AddScoped<ICampaignRepository, CampaignRepository>();
            services.AddScoped<ICampaignDetailRepository, CampaignDetailRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderItemRepository, OrderItemRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}

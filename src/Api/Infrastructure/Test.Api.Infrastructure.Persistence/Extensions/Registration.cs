using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Test.Api.Application.Interfaces.Repositories;
using Test.Api.Infrastructure.Persistence.Context;
using Test.Api.Infrastructure.Persistence.Repositories;

namespace Test.Api.Infrastructure.Persistence.Extensions
{
    public static class Registration
    {
        public static IServiceCollection AddInfrastructureRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TestContext>(conf =>
            {
                var connStr = configuration["SqlServer"].ToString();
                conf.UseSqlServer(connStr, opt =>
                {
                    opt.EnableRetryOnFailure();
                });
            });
            services.AddScoped<ICategoryRepository,CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IBillRepository, BillRepository>();
            services.AddScoped<IBillProductRelRepository, BillProductRelRepository>();
            services.AddScoped<IBillUserRelRepository, BillUserRelRepository>();
            services.AddScoped<IDiscountRepository, DiscountRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();

            return services;
        }
    }
}

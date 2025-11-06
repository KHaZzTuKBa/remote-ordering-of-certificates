using Application.Contracts;
using Infrastructure.Data;
using Infrastructure.Repo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection InfrastructureServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(
                    configuration.GetConnectionString("Default"),
                    b => b.MigrationsAssembly(typeof(ServiceContainer).Assembly.FullName)
                    ),
                ServiceLifetime.Scoped);

            services.AddScoped<IUser, UserRepo>();
            services.AddScoped<IAdmin, AdminRepo>();

            return services;
        }
    }
}

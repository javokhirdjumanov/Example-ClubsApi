using Football.Application.Services.UserServices;
using Football.Infrastructure.Context;
using Football.Infrastructure.Repositories.UserRepositories;
using Microsoft.EntityFrameworkCore;

namespace Football.Api.Extensions
{
    public static class ServicesCollectionExtensions
    {
        public static IServiceCollection AddDbContexts(
            this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("SqlServer");

            services.AddDbContextPool<AppDbContext>(options =>
            {
                options.UseSqlServer(connectionString, sqlServerOptions =>
                {
                    sqlServerOptions.EnableRetryOnFailure();
                });
            });

            return services;
        }
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IUserRepositiory, UserRepository>();

            return services;
        }
        public static IServiceCollection AddAplications(this IServiceCollection services)
        {
            services.AddScoped<IUserServices, UserServices>();
            services.AddSingleton<IUsersFactory, UserFactory>();

            return services;
        }
    }
}

using Football.Application.Services.Authentications;
using Football.Application.Services.UserServices;
using Football.Infrastructure.Authentication;
using Football.Infrastructure.Context;
using Football.Infrastructure.Repositories.UserRepositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
namespace Football.Api.Extensions
{
    public static class ServicesCollectionExtensions
    {
        /// <summary>
        /// A D D    Contexts     to DI Container
        /// </summary>
        public static IServiceCollection AddDbContexts(
            this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("SqlServer");

            services.Configure<Jwtoption>(configuration.GetSection("JwtSettings"));

            services.AddSwaggerService();

            services.AddDbContextPool<AppDbContext>(options =>
            {
                options.UseSqlServer(connectionString, sqlServerOptions =>
                {
                    sqlServerOptions.EnableRetryOnFailure();
                });
            });

            return services;
        }

        /// <summary>
        /// A D D   Aplications  to DI Container
        /// </summary>
        public static IServiceCollection AddAplications(this IServiceCollection services)
        {
            services.AddScoped<IUserServices, UserServices>();

            services.AddSingleton<IUsersFactory, UserFactory>();

            services.AddScoped<IAuthentications, Authentications>();

            services.AddHttpContextAccessor();

            return services;
        }
        /// <summary>
        /// A D D    Infrastructure   to DI Container
        /// </summary>
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IUserRepositiory, UserRepository>();
            services.AddTransient<IJwtTokenHandler, JwtTokenHandler>();
            services.AddSingleton<IPasswordHasher, PasswordHasher>();
            return services;
        }


        /// <summary>
        /// A D D   Authentication  to DI Container
        /// </summary>
        public static IServiceCollection AddAuthentications(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["JwtSettings:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"])),
                    ClockSkew = TimeSpan.Zero
                };
            });

            return services;
        }
        private static void AddSwaggerService(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Clubs.Api", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description =
                        "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });
            });
        }
    }
}

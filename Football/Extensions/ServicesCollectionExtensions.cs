using Football.Infrastructure.Repositories.UserRepositories;
using Football.Application.Services.Authentications;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Football.Application.Services.UserServices;
using Football.Infrastructure.Authentication;
using Football.Infrastructure.Context;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text;
using Serilog;

namespace Football.Api.Extensions
{
    public static class ServicesCollectionExtensions
    {
        /// <summary>
        /// Contexts     to DI Container
        /// </summary>
        public static IServiceCollection AddDbContexts(this IServiceCollection services, 
            IConfiguration configuration)
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
        /// Aplications  to DI Container
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
        /// Infrastructure   to DI Container
        /// </summary>
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IUserRepositiory, UserRepository>();
            services.AddTransient<IJwtTokenHandler, JwtTokenHandler>();
            services.AddSingleton<IPasswordHasher, PasswordHasher>();
            return services;
        }

        /// <summary>
        /// Authentication  to DI Container
        /// </summary>
        public static IServiceCollection AddAuthentications(this IServiceCollection services,
            IConfiguration configuration)
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
        
        /// <summary>
        /// Swagger Helper  to DI Container
        /// </summary>
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

        /// <summary>
        /// Add  Logging
        /// </summary>
        public  static WebApplicationBuilder AddLoggers(this WebApplicationBuilder builder, 
            IConfiguration configuration)
        {
            var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.FromLogContext()
                .CreateLogger();

            builder.Logging.ClearProviders();
            builder.Logging.AddSerilog(logger);

            return builder;
        }
    }
}
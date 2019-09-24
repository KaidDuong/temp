using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Rikkonbi.Infrastructure.Data;
using Rikkonbi.Infrastructure.Identity;
using Rikkonbi.WebAPI.Filters;
using Rikkonbi.WebAPI.Helpers;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rikkonbi.WebAPI.Extensions
{
    /// <summary>
    /// Service collection extensions
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// DbContext configurations
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RikkonbiDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("RikkonbiDbContext"), b => b.MigrationsAssembly("Rikkonbi.Infrastructure"));
            });
        }

        /// <summary>
        /// ASP.NET Identity configurations
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureAspNetIdentity(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddRoles<ApplicationRole>()
                .AddEntityFrameworkStores<RikkonbiDbContext>()
                .AddDefaultTokenProviders();
        }

        /// <summary>
        /// JWT authentication configurations
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var secret = configuration.GetSection("AppSettings").Get<AppSettings>().Secret;
            var key = Encoding.ASCII.GetBytes(secret);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ClockSkew = TimeSpan.FromMinutes(1),   // Clock skew compensates for server time drift (recommended: 5 minutes or less).
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    RequireSignedTokens = true,
                    RequireExpirationTime = true,
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true
                };
            });
        }

        /// <summary>
        /// Authorization configurations
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(Policies.ADMIN_OR_SALES, policy => policy.RequireRole(Roles.ADMIN, Roles.SALES));
            });
        }

        /// <summary>
        /// AutoMapper configurations
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        /// <summary>
        /// Swagger configurations
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Rikkonbi API", Version = "Ver.1.0" });
                c.OperationFilter<FormFileSwaggerFilter>();
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme()
                {
                    Description = "JWT Authorization header {token}",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    { "Bearer", new string[] { } }
                });
            });
        }
    }
}
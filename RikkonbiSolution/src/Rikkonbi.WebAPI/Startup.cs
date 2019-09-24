using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Rikkonbi.Core.Interfaces;
using Rikkonbi.Infrastructure.Data;
using Rikkonbi.Infrastructure.Services;
using Rikkonbi.WebAPI.Extensions;
using Rikkonbi.WebAPI.Filters;
using Rikkonbi.WebAPI.Interfaces;
using Rikkonbi.WebAPI.Services;
using System.IO;

namespace Rikkonbi.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Configure DbContext
            services.ConfigureDbContext(Configuration);

            // Configure ASP.NET Identity
            services.ConfigureAspNetIdentity();

            // Configure JWT authentication
            services.ConfigureJwtAuthentication(Configuration);

            // Configure authorization
            services.ConfigureAuthorization();

            // Configure AutoMapper
            services.ConfigureAutoMapper();

            // Configure Swagger
            services.ConfigureSwagger();

            // Configure DI for application services
            services.AddScoped<ISocialAuthService, GoogleAuthService>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
            services.AddScoped<IReportRepository, ReportRepository>();
            services.AddTransient<IReportService, ReportService>();
            services.AddScoped<IPaymentStatusRepository, PaymentStatusRepository>();
            services.AddSingleton<ITokenService>(new TokenService(Configuration));

            services.AddScoped<IDeviceCategoryRepository, DeviceCategoryRepository>();
            services.AddScoped<IDeviceRepository, DeviceRepository>();
            services.AddScoped<IBorrowRepository, BorrowRepository>();

            services.AddCors();

            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ValidateModelStateAttribute));
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Rikkonbi API Ver.1.0");
            });

            // Global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
#if DEBUG
            app.UseHttpsRedirection();
#endif
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Resources")),
                RequestPath = new PathString("/Resources")
            });
            app.UseSpaStaticFiles();
            app.UseMvc();
            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";
            });
        }
    }
}
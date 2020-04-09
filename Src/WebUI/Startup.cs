using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Northwind.Persistence;
using Northwind.WebUI.Settings;

namespace Northwind.WebUI
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
            services.AddDbContext<NorthwindDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("NorthwindDatabase")));
            services.AddControllers();
            services.AddOptions();
            services.AddSwagger(Configuration);
            services.AddApiVersioning(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger(Configuration);
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

    public static class ServicesConfiguration
    {
        public static void AddSwagger(this IServiceCollection services, IConfiguration Configuration)
        {
            services.Configure<SwaggerSettings>(Configuration);

            services.AddSwaggerGen(c =>
            {
                var settings = new SwaggerSettings();
                Configuration.GetSection(nameof(SwaggerSettings)).Bind(settings);

                c.SwaggerDoc(settings.SwaggerDoc.Name,
                    new OpenApiInfo
                    {
                        Title = settings.SwaggerDoc.OpenApiInfo.Title,
                        Version = settings.SwaggerDoc.OpenApiInfo.Version
                    });
            });
        }

        public static void AddApiVersioning(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddApiVersioning(c =>
            {
                c.DefaultApiVersion = new ApiVersion(1, 1);
                c.AssumeDefaultVersionWhenUnspecified = true;
                c.ReportApiVersions = true;
                c.ApiVersionReader = ApiVersionReader.Combine(
                    new QueryStringApiVersionReader("v"),
                    new HeaderApiVersionReader("X-Version")
                    );
            });
        }
    }

    public static class AppConfiguration
    {
        public static void UseSwagger(this IApplicationBuilder app, IConfiguration Configuration)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                var settings = new SwaggerSettings();
                Configuration.GetSection(nameof(SwaggerSettings)).Bind(settings);

                c.SwaggerEndpoint(settings.SwaggerEndpoint.Url, settings.SwaggerEndpoint.Name);
                c.RoutePrefix = string.Empty;
            });
        }
    }
}

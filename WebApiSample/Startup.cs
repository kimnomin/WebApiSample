using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Linq;
using System.Reflection;
using WebApiSample.Helper;

namespace WebApiSample
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
            #region Api Versioning
            services.AddApiVersioning(config =>
            {
                // 기본 API 버전을 1.0으로 지정
                config.DefaultApiVersion = new ApiVersion(1, 0);
                // 사용자가 버전을 지정하지 않은 경우, 기본 API 버전 사용
                config.AssumeDefaultVersionWhenUnspecified = true;
                // 특정 엔드 포인트에 지원되는 API 버전을 알림
                config.ReportApiVersions = true;
            });
            #endregion

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();
                c.SwaggerDoc("v1", new OpenApiInfo() { Title = "Api version  1", Version = "v1", Description = "Test Description", });
                c.SwaggerDoc("v2", new OpenApiInfo() { Title = "Api version  2", Version = "v2", Description = "Test Description", });

                c.OperationFilter<SwaggerParameterFilters>();
                c.DocumentFilter<SwaggerVersionMapping>();

                c.DocInclusionPredicate((version, desc) =>
                {
                    if (!desc.TryGetMethodInfo(out MethodInfo methodInfo)) return false;
                    var versions = methodInfo.DeclaringType.GetCustomAttributes(true).OfType<ApiVersionAttribute>().SelectMany(attr => attr.Versions);
                    var maps = methodInfo.GetCustomAttributes(true).OfType<MapToApiVersionAttribute>().SelectMany(attr => attr.Versions).ToArray();
                    version = version.Replace("v", "");
                    return versions.Any(v => v.ToString() == version && maps.Any(v => v.ToString() == version));
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger(options => options.RouteTemplate = "swagger/{documentName}/swagger.json");
            app.UseSwaggerUI(c =>
            {
                c.DocumentTitle = "Test Title";
                c.SwaggerEndpoint($"/swagger/v1/swagger.json", $"Version 1");
                c.SwaggerEndpoint($"/swagger/v2/swagger.json", $"Version 2");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

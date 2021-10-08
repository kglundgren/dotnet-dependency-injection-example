using DIExample.Api.Managers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DIExample.Api
{
    public class Startup
    {
        private readonly bool DevEnvProd;
        private readonly string CustomPolicy;
        private readonly string[] AllowedOrigins;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            DevEnvProd = bool.Parse(Configuration.GetSection("DevEnv")["Prod"]);
            CustomPolicy = "customPolicy";
            AllowedOrigins = Configuration.GetSection(nameof(AllowedOrigins)).GetChildren().Select(s => s.Value).ToArray();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DIExample.Api", Version = "v1" });
            });
            ConfigDeveloperEnvironment(services);
            services.AddCors(options => {
                options.AddPolicy(name: CustomPolicy, builder => {
                    builder.WithOrigins(AllowedOrigins)
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });
        }

        private void ConfigDeveloperEnvironment(IServiceCollection services)
        {
            // Dependency injection
            if (DevEnvProd) {
                services.AddScoped<ICustomerManager, RealDbCustomerManager>();
            } else {
                services.AddScoped<ICustomerManager, MockDbCustomerManager>();
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DIExample.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(CustomPolicy);

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}

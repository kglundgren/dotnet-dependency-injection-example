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

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            DevEnvProd = bool.Parse(Configuration.GetSection("DevEnv")["Prod"]);
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IsepOrgRanker.Business.Infra.Persistence.Ports;
using IsepOrgRanker.Business.Services;
using IsepOrgRanker.Persistence.InMemory.Repositories;
using IsepOrgRanker.Persistence.Csv.Repositories;

namespace IsepOrgRanker.Api
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

            if (Configuration["Persistence"] == "csv")
            {
                services.AddTransient<IOrganizationRepository, OrganizationCsvRepository>();
            }
            else
            {
                services.AddSingleton<IOrganizationRepository, OrganizationInMemoryRepository>();
            }
            services.AddTransient<OrganizationService>();

            services.AddControllers();
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

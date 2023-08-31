using ContractManager.Data;
using ContractManager.Data.Interfaces;
using ContractManager.Data.Repositories;
using ContractManager.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractManager
{
    public class Startup
    {
        private static IConfigurationRoot _conString;
        public Startup(IWebHostEnvironment host)
        {
            _conString = new ConfigurationBuilder().SetBasePath(host.ContentRootPath).AddJsonFile("Settings/DBSettings.json").Build();
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddMvc();
            services.AddDatabase(_conString.GetSection("DataBase"));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

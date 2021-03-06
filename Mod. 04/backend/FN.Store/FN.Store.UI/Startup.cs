﻿using FN.Store.Data.EF;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace FN.Store.UI
{
    public class Startup
    {
        //public Startup(IConfiguration config)
        //{}

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            Infra.CrossCuting.IoC.Configuration.RegisterServices(services);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
            
        }
    }
}

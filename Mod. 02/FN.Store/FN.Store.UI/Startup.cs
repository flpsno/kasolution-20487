using FN.Store.Data.EF;
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

            // Gera toda vez uma nova instância 
            // services.AddTransient

            // Gera uma única instância por requisição
            // services.AddScoped

            // Uma instância única por aplicação
            // services.AddSingleton

            services.AddScoped<StoreDataContext>();
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace FN.Store.Api
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Adicione ao pipeline o mvc
            services.AddMvc();

            // Configurando a Injeção de Dependências
            Infra.CrossCuting.IoC.Configuration.RegisterServices(services);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Libera o uso da api para clientes javascript
            app.UseCors(options => {
                options.AllowAnyOrigin();
                options.AllowAnyMethod();
                options.AllowAnyHeader();
            });

            // configura o mvc p/ verificar a requisição
            app.UseMvc();

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}

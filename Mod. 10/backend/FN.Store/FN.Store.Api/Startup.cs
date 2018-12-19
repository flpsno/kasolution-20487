using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FN.Store.Api
{
    public class Startup
    {
        private readonly IConfiguration _config;
        public Startup(IConfiguration config) => _config = config;

        public void ConfigureServices(IServiceCollection services)
        {
            // Adicione ao pipeline o mvc
            services.AddMvc();

            // Bearer ou Basic (Usuario|Senha) em Base64
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => {
                    options.TokenValidationParameters = new TokenValidationParameters() {

                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = "fansoft",
                        ValidAudience = "fansoft/client",

                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(_config["SecurityKey"])
                        ),

                        ClockSkew = System.TimeSpan.Zero


                    };

                    options.Events = new JwtBearerEvents() {

                        OnTokenValidated = context => {
                            // ctx.Log.Add(new Log(){})
                            // ctx.Savechanges()
                            // Debug.WriteLine("usuário autenticado: " + context.HttpContext.User.Claims);
                            return Task.CompletedTask;
                        },

                        OnAuthenticationFailed = context => {
                            // ctx.Log.Add(new Log(){})
                            // ctx.Savechanges()
                            // Debug.WriteLine("usuário não autenticado: " + context.HttpContext.User.Claims);
                            return Task.CompletedTask;
                        }


                    };
                });

            // Add Swagger
            services.AddSwaggerGen(s => {

                s.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info() {
                    Title = "FN Store - Doc",
                    Version = "v1",
                    Contact = new Swashbuckle.AspNetCore.Swagger.Contact()
                    {
                        Email = "fabiano.nalin@gmail.com",
                        Name = "Fabiano Nalin",
                        Url = "http://fansoft.com.br"
                    }
                });


                var security = new Dictionary<string, IEnumerable<string>>
                {
                    { "Bearer", new string[] { }}
                };

                s.AddSecurityDefinition("Bearer", new ApiKeyScheme() {
                    Description = "Entre com o token<br>(NÃO ESQUEÇA DO <strong>bearer</strong> na frente)",
                    Name = "Authorization",
                    In= "header",
                    Type = "apiKey"
                });

                s.AddSecurityRequirement(security);


            });

            // Configurando a Injeção de Dependências
            Infra.CrossCuting.IoC.Configuration.RegisterServices(services);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                var uploadFolder = System.IO.Path.Combine(env.WebRootPath, "uploads");
                System.IO.Directory.CreateDirectory(uploadFolder);
            }

            // Libera o uso da api para clientes javascript
            app.UseCors(options => {
                options.AllowAnyOrigin();
                options.AllowAnyMethod();
                options.AllowAnyHeader();
            });

            //Deve ser antes do MVC
            app.UseAuthentication();

            // configura o mvc p/ verificar a requisição
            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(s => {
                // By Matias
                s.SwaggerEndpoint("../swagger/v1/swagger.json", "FNStoreAPI");
                s.RoutePrefix = "docs";
            });
            
        }
    }
}

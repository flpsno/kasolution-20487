using FN.Store.Data.EF;
using FN.Store.Data.EF.Repositories;
using FN.Store.Domain.Contracts.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace FN.Store.Infra.CrossCuting.IoC
{
    // classe que contêm toda a configuração de injeção de dependências dos projetos
    public class Configuration
    {

        public static void RegisterServices(IServiceCollection services)
        {
            // Gera toda vez uma nova instância 
            // services.AddTransient

            // Gera uma única instância por requisição
            // services.AddScoped

            // Uma instância única por aplicação
            // services.AddSingleton

            services.AddScoped<StoreDataContext>();
            services.AddTransient<IProdutoRepository, ProdutoEFRepository>();
            services.AddTransient<ITipoProdutoRepository, TipoProdutoEFRepository>();


        }

    }
}

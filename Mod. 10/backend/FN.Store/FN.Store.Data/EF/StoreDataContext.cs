using FN.Store.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FN.Store.Data.EF
{
    public class StoreDataContext:DbContext
    {

        private readonly IConfiguration _config;

        // EF6
        //public StoreDataContext():base("StoreConn")
        //{}

        public StoreDataContext(IConfiguration config)
        {
            _config = config;
            Database.EnsureCreated();
        }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_config.GetConnectionString("StoredbConn"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new Maps.ProdutoMap());
            modelBuilder.ApplyConfiguration(new Maps.TipoProdutoMap());
            modelBuilder.ApplyConfiguration(new Maps.UsuarioMap());

            modelBuilder.Seed();
            
        }


    }
}

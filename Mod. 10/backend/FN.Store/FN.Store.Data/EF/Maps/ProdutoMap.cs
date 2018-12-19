using FN.Store.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FN.Store.Data.EF.Maps
{
    public class ProdutoMap : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {

            //Table
            builder.ToTable(nameof(Produto));

            //PK
            builder.HasKey(pk => pk.Id);
            // builder.HasKey(pk => new { pk.Id, pk.Nome }); --> PK composta

            //Colunas
            builder.Property(c => c.Nome)
                .IsRequired()
                .HasColumnType("varchar(80)");

            builder.Property(c => c.NomeArquivo)
                .IsRequired()
                .HasColumnType("varchar(255)");

            builder.Property(c => c.Preco)
                .HasColumnType("money");

            //Relacionamentos
            builder
                .HasOne(c => c.TipoProduto)
                .WithMany(c => c.Produtos).HasForeignKey(fk => fk.TipoProdutoId);

        }
    }
}

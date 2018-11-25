using FN.Store.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FN.Store.Data.EF.Maps
{
    public class TipoProdutoMap : IEntityTypeConfiguration<TipoProduto>
    {
        public void Configure(EntityTypeBuilder<TipoProduto> builder)
        {

            //Table
            builder.ToTable(nameof(TipoProduto));

            //PK
            builder.HasKey(pk => pk.Id);
            // builder.HasKey(pk => new { pk.Id, pk.Nome }); --> PK composta

            //Colunas
            builder.Property(c => c.Nome)
                .IsRequired()
                .HasColumnType("varchar(80)");

            //Relacionamentos


        }
    }
}

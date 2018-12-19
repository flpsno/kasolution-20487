using FN.Store.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FN.Store.Data.EF.Maps
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable(nameof(Usuario));
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).ValueGeneratedOnAdd();

            builder.Property(c => c.Nome).IsRequired().HasColumnType("varchar(100)");
            builder.Property(c => c.Email).IsRequired().HasColumnType("varchar(100)");
            builder.Property(c => c.Senha).IsRequired().HasColumnType("char(88)");

        }
    }
}

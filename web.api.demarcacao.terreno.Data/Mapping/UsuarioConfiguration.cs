using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using web.api.demarcacao.terreno.Domain.Entities;

namespace web.api.demarcacao.terreno.Data.Mapping
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("USUARIO");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id)
                .HasColumnName("ID")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(o => o.Nome)
                .HasColumnName("NOME")
                .IsRequired();

            builder.Property(o => o.Login)
                .HasColumnName("LOGIN")
                .IsRequired();

            builder.Property(o => o.Password)
                .HasColumnName("PASSWORD")
                .IsRequired();
        }
    }
}

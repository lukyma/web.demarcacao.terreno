using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using web.api.demarcacao.terreno.Domain.Entities;

namespace web.api.demarcacao.terreno.Data.Mapping
{
    public class UsuarioInterfaceConfiguration : IEntityTypeConfiguration<UsuarioInterface>
    {
        public void Configure(EntityTypeBuilder<UsuarioInterface> builder)
        {
            builder.ToTable("USUARIO_INTERFACE");

            builder.HasKey(o => new { o.IdUsuario, o.IdInterface });

            builder.Property(o => o.IdInterface)
                .HasColumnName("ID_INTERFACE")
                .IsRequired();

            builder.Property(o => o.IdUsuario)
                .HasColumnName("ID_USUARIO")
                .IsRequired();

            builder.HasOne(o => o.Interface)
                .WithMany(o => o.UsuarioInterfaces)
                .HasForeignKey(o => o.IdInterface);

            builder.HasOne(o => o.Usuario)
                .WithMany(o => o.UsuarioInterfaces)
                .HasForeignKey(o => o.IdUsuario);

            builder.HasOne(o => o.Interface)
                .WithMany(o => o.UsuarioInterfaces)
                .HasForeignKey(o => o.IdInterface);
        }
    }
}

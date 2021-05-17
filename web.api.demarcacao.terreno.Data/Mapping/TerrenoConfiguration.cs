using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using web.api.demarcacao.terreno.Domain.Entities;

namespace web.api.demarcacao.terreno.Data.Mapping
{
    public class TerrenoConfiguration : IEntityTypeConfiguration<Terreno>
    {
        public void Configure(EntityTypeBuilder<Terreno> builder)
        {
            builder.ToTable("TERRENO");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id)
                .HasColumnName("ID")
                .ValueGeneratedOnAdd();

            builder.Property(o => o.Descricao)
                .HasColumnName("DESCRICAO")
                .HasMaxLength(60)
                .IsRequired();

            builder.Property(o => o.IdEmpreendimento)
                .HasColumnName("ID_EMPREENDIMENTO")
                .IsRequired();

            builder.HasOne(o => o.Empreendimento)
                .WithMany(o => o.Terrenos)
                .HasForeignKey(o => o.IdEmpreendimento)
                .IsRequired();

            builder.Ignore(o => o.SomaDistanciaPontos);

            builder.Ignore(o => o.AreaTotal);
        }
    }
}

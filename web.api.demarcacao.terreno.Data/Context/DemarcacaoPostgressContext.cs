using Microsoft.EntityFrameworkCore;
using web.api.demarcacao.terreno.Data.Mapping;

namespace web.api.demarcacao.terreno.Data.Context
{
    public class DemarcacaoPostgressContext : DbContext, IDemarcacaoPostgressContext
    {
        public DemarcacaoPostgressContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EmpreendimentoConfiguration());
            modelBuilder.ApplyConfiguration(new TerrenoConfiguration());
            modelBuilder.ApplyConfiguration(new CoordenadaConfiguration());
            modelBuilder.ApplyConfiguration(new UsuarioConfiguration());
            modelBuilder.ApplyConfiguration(new InterfaceConfiguration());
            modelBuilder.ApplyConfiguration(new UsuarioInterfaceConfiguration());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}

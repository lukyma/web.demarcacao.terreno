using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;

namespace web.api.demarcacao.terreno.Data.Context.Factory
{
    public class DemarcacaoPostgressContextFactory : IDesignTimeDbContextFactory<DemarcacaoPostgressContext>
    {
        public DemarcacaoPostgressContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DemarcacaoPostgressContext>();
            var connectionString = $"Server={Environment.GetEnvironmentVariable("hostDd")};" +
                                       $"Port={Environment.GetEnvironmentVariable("portDb")};" +
                                       $"User Id={Environment.GetEnvironmentVariable("userNameDb")};" +
                                       $"Password={Environment.GetEnvironmentVariable("passwordDb")};" +
                                       $"Database={Environment.GetEnvironmentVariable("databaseNameDb")};" +
                                       $"SSL Mode=Prefer;Trust Server Certificate=true";
            optionsBuilder.UseNpgsql(connectionString);
            return new DemarcacaoPostgressContext(optionsBuilder.Options);
        }
    }
}

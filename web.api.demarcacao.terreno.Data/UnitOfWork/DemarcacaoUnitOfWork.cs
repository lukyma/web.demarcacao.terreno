using Microsoft.EntityFrameworkCore.Storage;
using System.Threading;
using System.Threading.Tasks;
using web.api.demarcacao.terreno.CrossCutting.Core;
using web.api.demarcacao.terreno.Data.Context;

namespace web.api.demarcacao.terreno.Data
{
    public class DemarcacaoUnitOfWork : IDemarcacaoUnitOfWork
    {
        private IDemarcacaoPostgressContext DbContext { get; }

        public DemarcacaoUnitOfWork(IDemarcacaoPostgressContext context)
        {
            DbContext = context;
        }

        public IDbContextTransaction BeginTransaction()
        {
            return DbContext.Database.BeginTransaction();
        }

        public int SaveChanges()
        {
            return DbContext.SaveChanges();
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return DbContext.SaveChangesAsync(cancellationToken);
        }
    }
}

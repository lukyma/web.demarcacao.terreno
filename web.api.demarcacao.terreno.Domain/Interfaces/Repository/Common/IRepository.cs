using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using web.api.demarcacao.terreno.Domain.Entities.Core;

namespace web.api.demarcacao.terreno.Domain.Interfaces.Repository.Common
{
    public interface IRepository<TEntity, TKey> where TEntity : class, IBaseEntity<TKey>

    {
        void Add(TEntity entity);
        Task AddAsync(TEntity entity, CancellationToken cancellationToken);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        TEntity Get(TKey id);
        Task<TEntity> GetAsync(TKey id, CancellationToken cancellationToken);
        bool Any(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> All(bool @readonly = false);
        Task<IEnumerable<TEntity>> AllAsync(CancellationToken cancellationToken, bool @readonly = false);
        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, bool @readonly = false);
    }
}

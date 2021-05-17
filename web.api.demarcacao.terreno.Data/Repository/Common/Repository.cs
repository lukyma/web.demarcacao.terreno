using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using web.api.demarcacao.terreno.Data.Context;
using web.api.demarcacao.terreno.Domain.Entities.Core;
using web.api.demarcacao.terreno.Domain.Interfaces.Repository.Common;

namespace web.api.demarcacao.terreno.Data.Repository.Common
{
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class, IBaseEntity<TKey>
    {
        private IDemarcacaoPostgressContext _dbContext { get; }
        private DbSet<TEntity> _dbSet { get; }

        public Repository(IDemarcacaoPostgressContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        protected IDemarcacaoPostgressContext Context
        {
            get { return _dbContext; }
        }

        protected DbSet<TEntity> DbSet
        {
            get { return _dbSet; }
        }
        public virtual void Add(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public virtual Task AddAsync(TEntity entity, CancellationToken cancellationToken)
        {
            return DbSet.AddAsync(entity, cancellationToken).AsTask();
        }

        public virtual void Delete(TEntity entity)
        {
            DbSet.Remove(entity);
        }

        public TEntity Get(TKey id)
        {
            return DbSet.Find(id);
        }

        public Task<TEntity> GetAsync(TKey id, CancellationToken cancellationToken)
        {
            return DbSet.FindAsync(new object[] { id }, cancellationToken).AsTask();
        }

        public bool Any(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Any(predicate);
        }

        public virtual void Update(TEntity entity)
        {
            Context.Update(entity);
        }

        public virtual IEnumerable<TEntity> All(bool @readonly = false)
        {
            return @readonly
                ? DbSet.AsNoTracking().ToList()
                : DbSet.ToList();
        }
        public virtual async Task<IEnumerable<TEntity>> AllAsync(CancellationToken cancellationToken, bool @readonly = false)
        {
            return @readonly
                ? await DbSet.AsNoTracking().ToListAsync(cancellationToken)
                : await DbSet.ToListAsync(cancellationToken);
        }

        public virtual IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, bool @readonly = false)
        {
            return @readonly
                ? DbSet.Where(predicate).AsNoTracking()
                : DbSet.Where(predicate);
        }
    }
}

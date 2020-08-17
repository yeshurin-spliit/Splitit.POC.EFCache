using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using EFCache.POC.DatabaseAccess.Entities;

namespace EFCache.POC.DatabaseAccess.Repositories
{
    public interface IGenericRepository<TEntity> : IGenericReadOnlyRepository<TEntity>
        where TEntity : class, IEntity
    {
        void Add(TEntity entity);
        void AddRange(List<TEntity> entities);
        void Update(TEntity entity);
        void Remove(TEntity entity);
        void Remove(Expression<Func<TEntity, bool>> predicate);

    }
}
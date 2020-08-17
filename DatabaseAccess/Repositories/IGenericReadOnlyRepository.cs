using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using EFCache.POC.DatabaseAccess.Entities;

namespace EFCache.POC.DatabaseAccess.Repositories
{
    public interface IGenericReadOnlyRepository<TEntity>
        where TEntity : class, IEntity
    {
        List<TEntity> GetAll(int take = 0,
            int skip = 0,
            Expression<Func<TEntity, object>> orderBy = null,
            OrderingDirection orderingDirection = OrderingDirection.Asc);

        List<TEntity> Filter(Expression<Func<TEntity, bool>> predicate,
            int take = 0,
            int skip = 0,
            Expression<Func<TEntity, object>> orderBy = null,
            OrderingDirection orderingDirection = OrderingDirection.Asc);

        TEntity First(Expression<Func<TEntity, bool>> predicate, int skip = 0);
        long Count();
        List<T> GetAllCertainProperty<T>(Expression<Func<TEntity, T>> selector);
    }
}
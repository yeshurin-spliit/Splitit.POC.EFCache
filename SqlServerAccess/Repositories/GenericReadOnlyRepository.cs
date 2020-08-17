using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using EFCache.POC.DatabaseAccess;
using EFCache.POC.DatabaseAccess.Entities;
using EFCache.POC.DatabaseAccess.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EFCache.POC.SqlServer.Repositories
{
    public class GenericReadOnlyRepository<TEntity> : IGenericReadOnlyRepository<TEntity>
        where TEntity : class, IEntity
    {
        private readonly DbContext _context;
        private readonly DbSet<TEntity> _dbSet;
        private readonly ContextLocker.ContextType _contextType;

        public GenericReadOnlyRepository(DbContext context, ContextLocker.ContextType contextType)
        {
            _context = context;
            _contextType = contextType;
            _context.ChangeTracker.AutoDetectChangesEnabled = false;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _dbSet = context.Set<TEntity>();
        }

        public List<T> GetAllCertainProperty<T>(Expression<Func<TEntity, T>> selector)
        {
            lock (ContextLocker.Instance.GetLockingObject(_contextType))
            {
                return _dbSet.Select(selector).ToList();
            }
        }

        public List<TEntity> Filter(Expression<Func<TEntity, bool>> predicate,
            int take = 0,
            int skip = 0,
            Expression<Func<TEntity, object>> orderBy = null,
            OrderingDirection orderingDirection = OrderingDirection.Asc)
        {
            lock (ContextLocker.Instance.GetLockingObject(_contextType))
            {
                var query = ApplySkipTakeAndOrder(take, skip, orderBy, orderingDirection);
                return query.Where(predicate).ToList();
            }
        }

        public TEntity First(Expression<Func<TEntity, bool>> predicate, int skip = 0)
        {
            lock (ContextLocker.Instance.GetLockingObject(_contextType))
            {
                var query = ApplySkipTakeAndOrder(0, skip);
                return query.FirstOrDefault(predicate);
            }
        }

        public long Count()
        {
            lock (ContextLocker.Instance.GetLockingObject(_contextType))
            {
                return _dbSet.Count();
            }
        }

        public List<TEntity> GetAll(int take = 0,
            int skip = 0,
            Expression<Func<TEntity, object>> orderBy = null,
            OrderingDirection orderingDirection = OrderingDirection.Asc)
        {
            lock (ContextLocker.Instance.GetLockingObject(_contextType))
            {
                var query = ApplySkipTakeAndOrder(take, skip, orderBy, orderingDirection);
                return query.ToList();
            }
        }

        private IQueryable<TEntity> ApplySkipTakeAndOrder(int take,
            int skip,
            Expression<Func<TEntity, object>> orderBy = null,
            OrderingDirection orderingDirection = OrderingDirection.Asc)
        {
            IQueryable<TEntity> query = _dbSet.AsNoTracking();
            if (skip > 0)
                query = query.Skip(skip);
            if (take > 0)
                query = query.Take(take);

            if (orderBy == null)
                return query;

            var orderedQuery = orderingDirection == OrderingDirection.Asc
                ? query.OrderBy(orderBy)
                : query.OrderByDescending(orderBy);
            query = orderedQuery;

            return query;
        }
    }
}
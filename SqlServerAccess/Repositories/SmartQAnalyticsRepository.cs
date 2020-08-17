using EFCache.POC.DatabaseAccess.Entities;
using EFCache.POC.DatabaseAccess.Repositories;
using EFCache.POC.SqlServer.Contexts;

namespace EFCache.POC.SqlServer.Repositories
{
    public class SplititRepository<TEntity> : GenericRepository<TEntity>, ISplititRepository<TEntity>
        where TEntity : class, IEntity
    {
        public SplititRepository(SplititContext context) : base(context, ContextLocker.ContextType.SingleContext) { }
    }
}
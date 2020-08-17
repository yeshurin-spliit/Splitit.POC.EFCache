using EFCache.POC.DatabaseAccess.Entities;

namespace EFCache.POC.DatabaseAccess.Repositories
{
    public interface ISplititRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : class, IEntity
    {

    }
}
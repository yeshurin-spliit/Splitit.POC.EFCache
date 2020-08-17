using EFCache.POC.DatabaseAccess;
using Microsoft.EntityFrameworkCore;

namespace EFCache.POC.SqlServer
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;

        public UnitOfWork(DbContext context)
        {
            _context = context;
        }

        /// <inheritdoc />
        public void SaveChanges()
        {
            _context.SaveChanges(true);
        }
    }
}
using EFCache.POC.SqlServer.Contexts;

namespace EFCache.POC.SqlServer
{
    public class SplititUnitOfWork : UnitOfWork
    {
        public SplititUnitOfWork(SplititContext context) : base(context)
        {
        }
    }
}
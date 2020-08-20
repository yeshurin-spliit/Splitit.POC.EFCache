using EFCache.POC.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace EFCache.POC.InMemCacheProvider
{
    public class InMemBootstrap : IBootstrap
    {
        public void Register(IServiceCollection serviceCollection)
        {
            serviceCollection.AddDistributedMemoryCache();
        }

        public int Bootorder => int.MaxValue;
    }
}
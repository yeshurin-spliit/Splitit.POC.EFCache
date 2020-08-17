using EFCache.POC.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace RedisCacheProvider
{
    public class RedisBootstrap : IBootstrap
    {
        public void Register(IServiceCollection serviceCollection)
        {
            serviceCollection.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = "localhost";
                options.InstanceName = "SampleInstance";
            });
        }

        public int Bootorder => int.MaxValue;
    }
}
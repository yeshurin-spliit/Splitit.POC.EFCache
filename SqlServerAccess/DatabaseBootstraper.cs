using EFCache.POC.Configurations;
using EFCache.POC.DatabaseAccess;
using EFCache.POC.DatabaseAccess.Repositories;
using EFCache.POC.IoC;
using EFCache.POC.SqlServer.Contexts;
using EFCache.POC.SqlServer.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EFCache.POC.SqlServer
{
    public class DatabaseBootstraper : IBootstrap
    {
        private const int _defaultConnectionPoolSize = 128;
        /// <inheritdoc />
        public void Register(IServiceCollection serviceCollection)
        {
            var provider = serviceCollection.BuildServiceProvider();
            var sqlConfig = provider.GetService<IDatabaseConfiguration>();
            serviceCollection.AddEntityFrameworkSqlServer();

            serviceCollection.AddDbContextPool<SplititContext>((serviceProvider, options) =>
                {
                    options.UseSqlServer(sqlConfig.ApplicationDbConnectionString,
                        sqlServerOptions =>
                        {
                            if (sqlConfig.MaxRetryCount.HasValue)
                                sqlServerOptions.EnableRetryOnFailure(sqlConfig.MaxRetryCount.Value);

                            if (sqlConfig.TimeoutInSeconds.HasValue)
                                sqlServerOptions.CommandTimeout(sqlConfig.TimeoutInSeconds.Value);

                        });
                    options.UseInternalServiceProvider(serviceProvider);
                }, sqlConfig.ConnectionPoolSize ?? _defaultConnectionPoolSize);

            serviceCollection.AddTransient(typeof(ISplititRepository<>), typeof(SplititRepository<>));
            serviceCollection.AddTransient<IUnitOfWork, SplititUnitOfWork>();
            //serviceCollection.AddHealthChecks().AddSqlServer(sqlConfig.ApplicationDbConnectionString);
        }

        /// <inheritdoc />
        public int Bootorder => int.MaxValue;
    }
}
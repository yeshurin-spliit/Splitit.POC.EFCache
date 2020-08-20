using System;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;
using EFCache.POC.Configurations;
using EFCache.POC.DatabaseAccess.Repositories;
using EFCache.POC.IoC.Extensions;
using EFCache.POC.SqlServer.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic;

namespace EFCache.POC.Runner
{
    public class Startup
    {
        public Startup()
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appSettings.json", optional: true, reloadOnChange: true)
                .Build();

        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            var configuration = Configuration.GetSection("DatabaseConfiguration").Get<DatabaseConfiguration>();
            services.AddSingleton<IDatabaseConfiguration>(configuration);
            BootstrapersRegistration.Run(services);
            ServiceLocator.Current.SetLocatorProvider(services.BuildServiceProvider());
            var serviceProvider = services.BuildServiceProvider();
            var repository = serviceProvider.GetService<ISplititRepository<Currency>>();
            //var cacheInstances = serviceProvider.GetServices<IDistributedCache>().ToList();
            //cacheInstances.ForEach(a =>
            //{
            //    Console.WriteLine(a.GetType().Name);
            //});
            CallAndDelay(serviceProvider, 1000, 1);
            CallAndDelay(serviceProvider, 3000, 2);
            CallAndDelay(serviceProvider, 10000, 3);
            CallAndDelay(serviceProvider, 4000, 4);
            CallAndDelay(serviceProvider, 12000, 5);
            CallAndDelay(serviceProvider, 30000, 6);
            CallAndDelay(serviceProvider, 1000, 7);
            CallAndDelay(serviceProvider, 1000, 8);
            
        }

        private void CallAndDelay(ServiceProvider serviceProvider, int msToWait, int minId)
        {
            var repository = serviceProvider.GetService<ISplititRepository<Currency>>();
            repository.Filter(a => a.Id > minId);
            Task.WaitAll(Task.Delay(msToWait));
        }
    }
}
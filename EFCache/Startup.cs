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
            var cacheInstances = serviceProvider.GetServices<IDistributedCache>();

            var test = repository.Filter(a => a.Id > 1);

            Task.WaitAll(Task.Delay(30000));

            test = repository.Filter(a => a.Id > 2);

            Task.WaitAll(Task.Delay(35000));

            test = repository.Filter(a => a.Id > 3);
        }
    }
}